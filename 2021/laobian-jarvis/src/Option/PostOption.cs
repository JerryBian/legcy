using CommandLine;
using Laobian.Common.Base;
using Laobian.Common.Blog;
using Laobian.Jarvis.Model;
using Laobian.Jarvis.Post;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Laobian.Jarvis.Option
{
    /// <summary>
    /// Post option
    /// </summary>
    [Verb("post", HelpText = "Manage blog posts.")]
    public class PostOption : OptionBase
    {
        private readonly PostManager _postManager;

        /// <summary>
        /// Default empty constructor of <see cref="PostOption"/>
        /// </summary>
        public PostOption()
        {
            _postManager = new PostManager();
        }

        /// <summary>
        /// Create new post template.
        /// </summary>
        [Option('n', "new", HelpText = "Create new post template.")]
        public bool New { get; set; }

        /// <summary>
        /// Add new post(s).
        /// </summary>
        [Option('a', "add", HelpText = "Add new post(s).")]
        public bool Add { get; set; }

        /// <summary>
        /// Update existing post(s).
        /// </summary>
        [Option('u', "update", HelpText = "Update existing post(s).")]
        public bool Update { get; set; }

        /// <summary>
        /// Keep silent if single post adding/updating failed. 
        /// </summary>
        [Option('s', "silent", Default = true, HelpText = "Keep silent if single post adding/updating failed. ")]
        public bool Silent { get; set; }

        /// <summary>
        /// File path or folder path, relative(to current executing root) or absolute. 
        /// </summary>
        [Value(0, HelpText = "File path or folder path, relative(to current executing root) or absolute. ")]
        public string Path { get; set; }

        /// <inheritdoc />
        protected override async Task ExecuteInternalAsync()
        {
            var fullPath = PathHelper.GetFullPath(Path);

            if (New)
            {
                if (!PathHelper.IsPathFolder(fullPath))
                {
                    await JarvisOut.ErrorAsync($"For --new option, the path must be valid folder.");
                    return;
                }

                await NewPostAsync(fullPath);
                return;
            }

            if (Add)
            {
                if (PathHelper.IsPathFolder(fullPath))
                {
                    // add batch of posts
                    await JarvisOut.VerbAsync($"Attempt to add all posts at: {fullPath}");
                    foreach (var file in Directory.EnumerateFiles(fullPath, $"*{_postManager.MarkdownExtension}"))
                    {
                        await JarvisOut.InfoAsync("----------");
                        try
                        {
                            var postPath = _postManager.GetFullPath(file);
                            await JarvisOut.InfoAsync($"Attempt to add post at: {postPath}");
                            var post = await _postManager.GetPostAsync(postPath);
                            await JarvisOut.VerbAsync($"BlogPost is valid and parsed: {post}");
                            await _postManager.AddPostAsync(post, postPath);
                        }
                        catch (Exception ex)
                        {
                            if (!Silent)
                            {
                                await JarvisOut.ErrorAsync("Terminated - ", ex);
                                return;
                            }

                            await JarvisOut.InfoAsync($"Skipped - {ex.Message}");
                        }

                        await JarvisOut.InfoAsync("----------");
                    }
                }
                else
                {
                    if (!PathHelper.IsExtensionValid(fullPath, $"*{_postManager.MarkdownExtension}"))
                    {
                        await JarvisOut.ErrorAsync("Invalid post file extension.");
                        return;
                    }

                    await JarvisOut.VerbAsync($"Attempt to add new post at: {fullPath}");

                    var post = await _postManager.GetPostAsync(fullPath);
                    await JarvisOut.VerbAsync("BlogPost content is valid and parsed");
                    await _postManager.AddPostAsync(post, fullPath);
                }

                return;
            }

            if (Update)
            {
                if (PathHelper.IsPathFolder(fullPath))
                {
                    await JarvisOut.VerbAsync($"Attempt to update all posts at: {fullPath}");

                    foreach (var file in Directory.EnumerateFiles(fullPath, $"*{_postManager.MarkdownExtension}"))
                    {
                        await JarvisOut.InfoAsync("----------");
                        try
                        {
                            var postPath = _postManager.GetFullPath(file);
                            await JarvisOut.InfoAsync($"Attempt to update post at: {postPath}");
                            var post = await _postManager.GetPostAsync(postPath);
                            await JarvisOut.VerbAsync($"BlogPost is valid and parsed: {postPath}");
                            await _postManager.UpdatePostAsync(post, postPath);
                        }
                        catch (Exception ex)
                        {
                            if (!Silent)
                            {
                                await JarvisOut.ErrorAsync("Terminated - ", ex);
                                return;
                            }

                            await JarvisOut.InfoAsync($"Skipped - {ex.Message}");
                        }
                        await JarvisOut.InfoAsync("----------");
                    }
                }
                else
                {
                    await JarvisOut.VerbAsync($"Attempt to update post at: {fullPath}");

                    var post = await _postManager.GetPostAsync(fullPath);
                    await JarvisOut.VerbAsync($"BlogPost is valid and parsed: {fullPath}");
                    await _postManager.UpdatePostAsync(post, fullPath);
                }
            }
        }

        private async Task NewPostAsync(string fullPath)
        {
            await JarvisOut.VerbAsync($"Attempt to generate new post at: {fullPath}.");

            var post = new BlogPost();
            post.Raw.Id = Guid.NewGuid().Normal();
            post.Raw.Title = $"<Title Placeholder - {Guid.NewGuid().Normal()}>";
            post.Raw.Category = $"<Category Placeholder - {Guid.NewGuid().Normal()}>";
            post.Raw.Url = $"Url-Placeholder-{Guid.NewGuid().Normal()}";
            post.Raw.PublishTime = DateTime.UtcNow.ToDateAndTime();
            post.Raw.Publish = bool.FalseString;
            post.Raw.Excerpt = $"<Excerpt Placeholder(Markdown) - {Guid.NewGuid().Normal()}>";
            post.Raw.Content = $"<Content Placeholder(Markdown) - {Guid.NewGuid().Normal()}>";

            var content = PostParser.ToRawData(post);
            var savedPath = System.IO.Path.Combine(fullPath, _postManager.GetPostFileName(post.Raw.Id));
            await File.WriteAllTextAsync(savedPath, content, Encoding.UTF8);
            await JarvisOut.InfoAsync($"New post template created: {savedPath}");
        }
    }
}
