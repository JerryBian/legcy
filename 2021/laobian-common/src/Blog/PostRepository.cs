using Laobian.Common.Azure;
using Laobian.Common.Base;
using Laobian.Common.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laobian.Common.Setting;

namespace Laobian.Common.Blog
{
    /// <summary>
    /// Implementation of <see cref="IPostRepository"/>
    /// </summary>
    public class PostRepository : IPostRepository
    {
        private const string CacheKey = "LAOBIAN_POSTS";
        private readonly IAzureBlobClient _azureClient;
        private readonly ICacheClient _cacheClient;

        /// <summary>
        /// Constructor of <see cref="PostRepository"/>
        /// </summary>
        /// <param name="azureClient">Azure client</param>
        /// <param name="cacheClient">Cache client</param>
        public PostRepository(IAzureBlobClient azureClient, ICacheClient cacheClient)
        {
            _azureClient = azureClient;
            _cacheClient = cacheClient;
        }

        #region Implementation of IPostRepository

        /// <inheritdoc />
        public async Task<List<BlogPost>> GetPostsAsync()
        {
            if (!_cacheClient.TryGet(CacheKey, out List<BlogPost> posts))
            {
                posts = await _azureClient.DownloadAsync<List<BlogPost>>(
                    BlobContainer.Private,
                    PrivateBlobResolver.PostBlob(),
                    BlobType.ProtoBuf);
                if (posts == null)
                {
                    return new List<BlogPost>();
                }

                foreach (var i in SystemState.PostsVisitCount)
                {
                    var post = posts.FirstOrDefault(ps => ps.Id == i.Key);
                    post?.SetVisitCount(i.Value);
                }

                UpdatePostsCache(posts);
            }

            return posts;
        }

        /// <inheritdoc />
        public async Task<Guid> AddAsync(BlogPost blogPost)
        {
            if (blogPost.PublishTime == default)
            {
                blogPost.Raw.PublishTime = DateTime.UtcNow.ToDateAndTime();
            }

            if (blogPost.Id == default)
            {
                blogPost.Raw.Id = Guid.NewGuid().Normal();
            }

            blogPost.UpdateTime = DateTime.UtcNow;
            var posts = await GetPostsAsync();
            if (posts.FirstOrDefault(p => p.Id == blogPost.Id) != null)
            {
                throw new InvalidOperationException($"BlogPost with ID({blogPost.Id}) already exists.");
            }

            ValidatePost(blogPost, posts);

            posts.Add(blogPost);
            await UploadPostsAsync(posts);
            return blogPost.Id;
        }

        /// <inheritdoc />
        public async Task UpdateAsync(BlogPost blogPost)
        {
            var posts = await GetPostsAsync();
            if (blogPost.Id == default)
            {
                throw new InvalidOperationException(
                    "This blogPost has no ID assigned, it might be not created yet, try add-blogPost command instead.");
            }

            var existingPost = posts.FirstOrDefault(p => p.Id == blogPost.Id);
            if (existingPost == null)
            {
                throw new InvalidOperationException($"BlogPost with ID({blogPost.Id}) does not exists.");
            }

            if (blogPost.Raw.Content.Trim('\r', '\n').EqualsIgnoreCase(existingPost.Raw.Content.Trim('\r', '\n')))
            {
                blogPost.UpdateTime = existingPost.UpdateTime;
            }

            if (blogPost.UpdateTime == default)
            {
                blogPost.UpdateTime = DateTime.UtcNow;
            }

            posts.Remove(existingPost);
            ValidatePost(blogPost, posts);

            blogPost.SetVisitCount(existingPost.VisitCount);
            posts.Add(blogPost);
            await UploadPostsAsync(posts);
        }

        #endregion

        private void UpdatePostsCache(List<BlogPost> posts)
        {
            if (posts == null)
            {
                posts = new List<BlogPost>();
            }

            _cacheClient.Set(CacheKey, posts, AppSetting.Default.BlogPostReloadInterval);
            SystemState.LastTimePostReloaded = DateTime.UtcNow;
            SystemState.PublishedPostsCount = posts.Count(p => p.Publish);
        }

        private void ValidatePost(BlogPost blogPost, IReadOnlyCollection<BlogPost> posts)
        {
            if (posts.FirstOrDefault(p => p != blogPost && p.Url.EqualsIgnoreCase(blogPost.Url)) != null)
            {
                throw new InvalidOperationException($"BlogPost with URL({blogPost.Url}) already exists.");
            }

            if (posts.FirstOrDefault(p => p != blogPost && p.Title.EqualsIgnoreCase(blogPost.Url)) != null)
            {
                throw new InvalidOperationException($"BlogPost with Title({blogPost.Title}) already exists.");
            }
        }

        private async Task UploadPostsAsync(List<BlogPost> posts)
        {
            await _azureClient.UploadAsync(BlobContainer.Private, PrivateBlobResolver.PostBlob(), BlobType.ProtoBuf, posts);
            _cacheClient.Remove(CacheKey);
        }
    }
}