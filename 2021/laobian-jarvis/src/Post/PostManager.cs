using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Laobian.Common.Azure;
using Laobian.Common.Base;
using Laobian.Common.Blog;
using Laobian.Common.Cache;
using Laobian.Jarvis.Model;

namespace Laobian.Jarvis.Post
{
    /// <summary>
    /// Post manager for Jarvis
    /// </summary>
    public class PostManager
    {
        private readonly IPostRepository _postRepository;

        /// <summary>
        /// Default empty constructor of <see cref="PostManager"/>
        /// </summary>
        public PostManager()
        {
            _postRepository = new PostRepository(new AzureBlobClient(), new MemoryCacheClient());
        }

        /// <summary>
        /// Markdown file extension
        /// </summary>
        public string MarkdownExtension => ".md";

        /// <summary>
        /// Get full path for specified file
        /// </summary>
        /// <param name="file">The given file path</param>
        /// <returns>The full path</returns>
        public string GetFullPath(string file)
        {
            var path = Path.GetFullPath(file);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Target file is not exist: {path}");
            }

            return path;
        }

        /// <summary>
        /// Get all posts
        /// </summary>
        /// <returns>Collection of post</returns>
        public async Task<List<BlogPost>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetPostsAsync();
            await JarvisOut.VerbAsync($"Returned {posts.Count} posts from store");
            return posts;
        }

        /// <summary>
        /// Get <see cref="BlogPost"/> instance by given markdown file path
        /// </summary>
        /// <param name="fullPath">Full path of markdown file</param>
        /// <returns></returns>
        public async Task<BlogPost> GetPostAsync(string fullPath)
        {
            var content = await File.ReadAllTextAsync(fullPath);
            return await PostParser.ToPostAsync(content);
        }

        /// <summary>
        /// Add new post, update local version thereafter
        /// </summary>
        /// <param name="blogPost">Instance of new post</param>
        /// <param name="fullPath">Local file path</param>
        /// <returns>Task</returns>
        public async Task AddPostAsync(BlogPost blogPost, string fullPath)
        {
            var postId = await _postRepository.AddAsync(blogPost);
            await JarvisOut.InfoAsync($"add blogPost successfully, id is: {postId.Normal()}");

            await UpdateLocalAsync(blogPost, fullPath);
        }

        /// <summary>
        /// Update local markdown file
        /// </summary>
        /// <param name="blogPost">Instance of <see cref="BlogPost"/></param>
        /// <param name="fullPath">The given local markdown file path</param>
        /// <returns>Task</returns>
        public async Task UpdateLocalAsync(BlogPost blogPost, string fullPath)
        {
            fullPath = Path.GetFullPath(fullPath);
            if (File.Exists(fullPath))
            {
                await JarvisOut.VerbAsync($"File already exists, will backup first in case of failure: {fullPath}");

                // backup existing markdown
                var backupMdPath = Path.Combine(Path.GetTempPath(), "jarvis", "blogPost");
                Directory.CreateDirectory(backupMdPath);
                backupMdPath = Path.Combine(
                    backupMdPath,
                    Path.GetFileNameWithoutExtension(fullPath) + $"_{DateTime.UtcNow:yyyyMMddhhmmssfff}" +
                    Path.GetExtension(fullPath));
                File.Copy(fullPath, backupMdPath);
                await JarvisOut.VerbAsync($"Backup existing blogPost to: {backupMdPath}");

                try
                {
                    var updatedContent = PostParser.ToRawData(blogPost);
                    File.Delete(fullPath);
                    await JarvisOut.InfoAsync($"Existing blogPost is deleted: {fullPath}");
                    var newFilePath = Path.Combine(Path.GetDirectoryName(fullPath), GetPostFileName(blogPost.Raw.Url));
                    await File.WriteAllTextAsync(newFilePath, updatedContent, Encoding.UTF8);
                    await JarvisOut.InfoAsync($"Updated blogPost is saved: {newFilePath}");
                }
                catch (Exception ex)
                {
                    await JarvisOut.ErrorAsync("Saving to local failed - ", ex);
                    File.Copy(backupMdPath, fullPath);
                    await JarvisOut.InfoAsync($"Rollback to original blogPost: {fullPath}");
                }
            }
            else
            {
                await JarvisOut.VerbAsync($"File does not exist, will create: {fullPath}");
                var content = PostParser.ToRawData(blogPost);
                await File.WriteAllTextAsync(fullPath, content, Encoding.UTF8);
            }
        }

        /// <summary>
        /// Update existing post, then update local markdown file
        /// </summary>
        /// <param name="blogPost">Updated post</param>
        /// <param name="fullPath">Local markdown file</param>
        /// <returns>Task</returns>
        public async Task UpdatePostAsync(BlogPost blogPost, string fullPath)
        {
            await _postRepository.UpdateAsync(blogPost);
            await JarvisOut.InfoAsync($"updated blogPost successfully, id is: {blogPost.Raw.Id}");

            await UpdateLocalAsync(blogPost, fullPath);
        }

        /// <summary>
        /// Get file name
        /// </summary>
        /// <param name="name">The file name without extension</param>
        /// <returns>File name with extension</returns>
        public string GetPostFileName(string name)
        {
            return $"{name}{MarkdownExtension}";
        }

        /// <summary>
        /// Save post to local
        /// </summary>
        /// <param name="directory">Local post folder</param>
        /// <param name="blogPost">To be saved post</param>
        /// <returns>Task</returns>
        public async Task SaveToLocalAsync(string directory, BlogPost blogPost)
        {
            var path = Path.Combine(directory, GetPostFileName(blogPost.Url));
            await UpdateLocalAsync(blogPost, path);
            await JarvisOut.InfoAsync($"Pulled blogPost: {blogPost.Title}");
        }
    }
}
