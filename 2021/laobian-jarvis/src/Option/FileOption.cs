using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CommandLine;
using Laobian.Common.Azure;
using Laobian.Common.Base;
using Laobian.Jarvis.Model;

namespace Laobian.Jarvis.Option
{
    /// <summary>
    /// File option
    /// </summary>
    [Verb("file", HelpText = "Manage files in locally and remotely.")]
    public class FileOption:OptionBase
    {
        private readonly IAzureBlobClient _azureClient;

        /// <summary>
        /// Default empty constructor of <see cref="FileOption"/>
        /// </summary>
        public FileOption()
        {
            _azureClient = new AzureBlobClient();
        }

        /// <summary>
        /// List all files in store with metadata information displayed.
        /// </summary>
        [Option('l', "list", HelpText = "List all files in store with metadata information displayed.")]
        public bool List { get; set; }

        /// <summary>
        /// Add new file.
        /// </summary>
        [Option('a', "add", HelpText = "Add new file.")]
        public bool Add { get; set; }

        /// <summary>
        /// Download all remote files.
        /// </summary>
        [Option('d', "download", HelpText = "Download all remote files.")]
        public bool Download { get; set; }

        /// <summary>
        /// file path or folder path, relative(to current executing root) or absolute.
        /// </summary>
        [Value(0, HelpText = "File path or folder path, relative(to current executing root) or absolute.")]
        public string Path { get; set; }

        /// <inheritdoc />
        protected override async Task ExecuteInternalAsync()
        {
            if (List)
            {
                await JarvisOut.VerbAsync("Fall in List option.");

                var files = await ListFilesAsync();
                var filesCount = 0;
                var filesSize = 0L;
                foreach (var blobData in files)
                {
                    filesCount++;
                    filesSize += blobData.Size;
                    await JarvisOut.InfoAsync($"{blobData.BlobName}\t\t{FileSizeHelper.Format(filesSize)}\t{blobData.Created}");
                }

                await JarvisOut.InfoAsync($"{filesCount} files, {FileSizeHelper.Format(filesSize)} in total.");
                return;
            }

            if (Add)
            {
                var fullPath = PathHelper.GetFullPath(Path);
                if (PathHelper.IsPathFolder(fullPath))
                {
                    await JarvisOut.ErrorAsync("Supplied path must be valid file.");
                    return;
                }

                await JarvisOut.InfoAsync($"Attempt to upload file(prompt if exists): {fullPath}");

                var fileName = PathHelper.GetPathName(fullPath);
                if (await _azureClient.ExistAsync(BlobContainer.Public, fileName))
                {
                    await JarvisOut.InfoAsync("We found {0} already exists, do you want to override or cancel? Input O(o)/Y(y) to override, otherwise cancel.", fileName);
                    var choice = Console.ReadKey();
                    if (choice.Key != ConsoleKey.Y && choice.Key != ConsoleKey.O)
                    {
                        await JarvisOut.InfoAsync("Cancelled by user.");
                        Environment.Exit(1);
                    }
                    else
                    {
                        await JarvisOut.VerbAsync("You selected override.");
                    }
                }

                using (var stream = File.Open(fullPath, FileMode.Open))
                {
                    var url = await _azureClient.UploadAsync(BlobContainer.Public, fileName, BlobType.Other, stream);
                    await JarvisOut.InfoAsync("File uploaded successfully: {0}", url);
                }

                return;
            }

            if (Download)
            {
                var fullPath = PathHelper.GetFullPath(Path);
                if (!PathHelper.IsPathFolder(fullPath))
                {
                    await JarvisOut.ErrorAsync("Supplied path must be valid folder.");
                    return;
                }

                await JarvisOut.VerbAsync($"Attempt to download all posts to: {fullPath}");

                var files = new List<BlobData>(await ListFilesAsync());
                await JarvisOut.InfoAsync($"Attempt to download {files.Count} posts");

                foreach (var f in files)
                {
                    var path = PathHelper.Combine(fullPath, f.BlobName);
                    using (var fs = File.Create(path))
                    using (f.Stream)
                    {
                        f.Stream.Seek(0, SeekOrigin.Begin);
                        await f.Stream.CopyToAsync(fs);
                        await JarvisOut.InfoAsync("File downloaded: {0}", path);
                    }
                }

                await JarvisOut.InfoAsync("All files are downloaded to locally: {0}", fullPath);
            }
        }

        private async Task<IEnumerable<BlobData>> ListFilesAsync()
        {
            return await _azureClient.ListAsync(BlobContainer.Public);
        }
    }
}
