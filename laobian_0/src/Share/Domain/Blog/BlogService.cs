using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laobian.Share.Domain.Blog.RandomPost;
using Laobian.Share.Domain.Blog.Rss;
using Laobian.Share.Domain.Blog.Sitemap;
using Laobian.Share.Domain.Interface;
using Laobian.Share.Domain.Setting;
using Laobian.Share.Infrastructure.Interface;
using Laobian.Share.Model;
using Laobian.Share.Utility.Extension;
using Laobian.Share.Utility.Helper;

namespace Laobian.Share.Domain.Blog
{
    public class BlogService : IBlogService
    {
        private readonly IBlobDataRepository<BlogCategory> _blogCategoryRepository;
        private readonly IBlobDataRepository<BlogPostCategory> _blogPostCategoryRepository;
        private readonly IBlobDataRepository<BlogPost> _blogPostRepository;
        private readonly IBlobDataRepository<BlogPostVisit> _blogPostVisitRepository;
        private readonly IBlogPostVisitService _blogPostVisitService;
        private readonly ICacheClient _cacheClient;
        private readonly ISettingService _settingService;

        public BlogService(
            ICacheClient cacheClient,
            ISettingService settingService,
            IBlogPostVisitService blogPostVisitService,
            IBlobDataRepository<BlogPost> blogPostRepository,
            IBlobDataRepository<BlogCategory> blogCategoryRepository,
            IBlobDataRepository<BlogPostVisit> blogPostVisitRepository,
            IBlobDataRepository<BlogPostCategory> blogPostCategoryRepository)
        {
            _cacheClient = cacheClient;
            _settingService = settingService;
            _blogPostVisitService = blogPostVisitService;
            _blogPostRepository = blogPostRepository;
            _blogCategoryRepository = blogCategoryRepository;
            _blogPostVisitRepository = blogPostVisitRepository;
            _blogPostCategoryRepository = blogPostCategoryRepository;
        }

        public async Task<List<BlogCategory>> FindAllCategoryAsync()
        {
            return await _blogCategoryRepository.FindAllAsync();
        }

        public async Task AddCategoryAsync(BlogCategory blogCategory)
        {
            await _blogCategoryRepository.AddAsync(blogCategory);
            await ExpireAllRandomPostsKeysAsync();
        }

        public async Task UpdateCategoryAsync(BlogCategory blogCategory)
        {
            await _blogCategoryRepository.UpdateAsync(blogCategory);
            await ExpireAllRandomPostsKeysAsync();
        }

        public async Task<BlogCategory> FindCategoryAsync(Guid id)
        {
            return await _blogCategoryRepository.FindAsync(id);
        }

        public async Task<List<BlogPost>> FindAllPostAsync(bool? isPublish = null)
        {
            var posts = await _blogPostRepository.FindAsync(_ => isPublish == null || _.IsPublish == isPublish);
            var postCategoryMap = await _blogPostCategoryRepository.FindAllAsync();
            foreach (var p in posts)
            foreach (var pc in postCategoryMap.Where(pc => pc.PostId == p.Id))
            {
                var cat = await _blogCategoryRepository.FindAsync(pc.CategoryId);
                if (cat != null) p.BlogCategories.Add(cat);
            }

            return posts;
        }

        public async Task<BlogPost> FindPostAsync(Guid id)
        {
            var post = await _blogPostRepository.FindAsync(id);
            if (post != null)
            {
                var postCategoryMap = await _blogPostCategoryRepository.FindAllAsync();
                foreach (var pc in postCategoryMap.Where(pc => pc.PostId == id))
                {
                    var cat = await _blogCategoryRepository.FindAsync(pc.CategoryId);
                    if (cat != null) post.BlogCategories.Add(cat);
                }
            }

            return post;
        }

        public async Task UpdatePostAsync(BlogPost blogPost)
        {
            var existingPost = await _blogPostRepository.FindAsync(blogPost.Id);
            if (existingPost == null) return;

            var existingMap = await _blogPostCategoryRepository.FindAsync(pc => pc.PostId == blogPost.Id);
            await _blogPostCategoryRepository.RemoveAsync(existingMap);

            var newPostCategoryMap = new List<BlogPostCategory>();
            foreach (var cat in blogPost.BlogCategories)
            {
                var c = await _blogCategoryRepository.FindAsync(cat.Id);
                if (c != null)
                    newPostCategoryMap.Add(new BlogPostCategory
                    {
                        CategoryId = c.Id,
                        PostId = blogPost.Id
                    });
            }

            await Task.WhenAll(
                _blogPostRepository.UpdateAsync(blogPost, true),
                _blogPostCategoryRepository.AddRangeAsync(newPostCategoryMap),
                ExpireAllRandomPostsKeysAsync());
        }

        public async Task<BlogPost> FindPostAsync(int year, int month, string url)
        {
            var posts = await _blogPostRepository.FindAllAsync();
            var post = posts.FirstOrDefault(p =>
                p.CreateAt.Year == year && p.CreateAt.Month == month && p.Url.EqualIgnoreCase(url));
            if (post != null)
            {
                var postCategoryMap = await _blogPostCategoryRepository.FindAllAsync();
                foreach (var pc in postCategoryMap.Where(pc => pc.PostId == post.Id))
                {
                    var cat = await _blogCategoryRepository.FindAsync(pc.CategoryId);
                    if (cat != null) post.BlogCategories.Add(cat);
                }

                var pvs = await _blogPostVisitRepository.FindAllAsync();
                var pv = pvs.FirstOrDefault(_ => _.PostId == post.Id);
                if (pv != null) post.Visit = pv.Visit;

                posts = posts.OrderBy(_ => _.CreateAt).ToList();
                var firstPost = posts.FirstOrDefault();
                if (firstPost != null && firstPost.Id != post.Id) post.FirstPost = firstPost;

                var lastPost = posts.LastOrDefault();
                if (lastPost != null && lastPost.Id != post.Id) post.LastPost = lastPost;

                var prevPost = posts.ElementAtOrDefault(posts.IndexOf(post) - 1);
                if (prevPost != null) post.PrevPost = prevPost;

                var nextPost = posts.ElementAtOrDefault(posts.IndexOf(post) + 1);
                if (nextPost != null) post.NextPost = nextPost;
            }

            return post;
        }

        public async Task AddPostVisitAsync(int year, int month, string url)
        {
            var posts = await _blogPostRepository.FindAllAsync();
            var post = posts.FirstOrDefault(p =>
                p.CreateAt.Year == year && p.CreateAt.Month == month && p.Url.EqualIgnoreCase(url));
            if (post != null)
            {
                await AddPostVisitAsync(post.Id);
            }
        }

        public async Task AddPostVisitAsync(Guid postId)
        {
            await _blogPostVisitService.AddAsync(postId);
        }

        public async Task AddPostAsync(BlogPost blogPost)
        {
            var newPostCategoryMap = new List<BlogPostCategory>();
            foreach (var cat in blogPost.BlogCategories)
            {
                var c = await _blogCategoryRepository.FindAsync(cat.Id);
                if (c != null)
                    newPostCategoryMap.Add(new BlogPostCategory
                    {
                        CategoryId = c.Id,
                        PostId = blogPost.Id
                    });
            }

            await Task.WhenAll(
                _blogPostRepository.AddAsync(blogPost, true),
                _blogPostCategoryRepository.AddRangeAsync(newPostCategoryMap),
                ExpireAllRandomPostsKeysAsync());
        }

        public async Task<List<BlogPost>> FindAllByCategoryAsync(string categoryUrl, bool? isPublish = null)
        {
            var cats = await _blogCategoryRepository.FindAllAsync();
            var cat = cats.FirstOrDefault(c => c.Url.EqualIgnoreCase(categoryUrl));
            if (cat == null) return new List<BlogPost>();

            var postCategoryMap = await _blogPostCategoryRepository.FindAllAsync();
            var postIds = postCategoryMap.Where(pc => pc.CategoryId == cat.Id).Select(pc => pc.PostId);
            var posts = await _blogPostRepository.FindAllAsync();
            posts = posts.Where(p => postIds.Contains(p.Id) && (isPublish == null || p.IsPublish == isPublish))
                .ToList();

            foreach (var p in posts)
            foreach (var pc in postCategoryMap.Where(pc => pc.PostId == p.Id))
            {
                var c = await _blogCategoryRepository.FindAsync(pc.CategoryId);
                if (c != null) p.BlogCategories.Add(cat);
            }

            return posts;
        }

        public async Task<string> BuildRssAsync()
        {
            var results = await FindAllPostAsync(true);
            results = results.OrderByDescending(_ => _.CreateAt).ToList();
            var rss = new RssRoot
            {
                Version = "2.0",
                Channel = new RssChannel
                {
                    Title = await _settingService.FindAsync<string>(SettingKey.BlogDefaultTitle),
                    Copyright = await _settingService.FindAsync<string>(SettingKey.Copyright),
                    Docs = "http://www.rssboard.org/rss-specification",
                    Language = "en-US",
                    Link = "https://blog.laobian.me",
                    PubDate = DateTime.UtcNow,
                    Category = new List<string> {"blog", "life"},
                    WebMaster =
                        $"{await _settingService.FindAsync<string>(SettingKey.Email)} ({await _settingService.FindAsync<string>(SettingKey.Author)})",
                    ManagingEditor =
                        $"{await _settingService.FindAsync<string>(SettingKey.Email)} ({await _settingService.FindAsync<string>(SettingKey.Author)})",
                    Ttl = 60,
                    LastBuildDate = DateTime.UtcNow,
                    Description = await _settingService.FindAsync<string>(SettingKey.BlogDefaultDescription)
                }
            };
            foreach (var blogPost in results)
            {
                var item = new ChannelItem
                {
                    Author = await _settingService.FindAsync<string>(SettingKey.Email),
                    Guid = blogPost.GetFullUrl(),
                    Link = blogPost.GetFullUrl(),
                    Description = blogPost.Html,
                    Title = blogPost.Title,
                    PubDate = blogPost.CreateAt
                };

                rss.Channel.Items.Add(item);
            }

            return SerializeHelper.SerializeToXml(rss);
        }

        public async Task<string> BuildSitemapAsync()
        {
            var sitemap = new SitemapUrlSet();
            sitemap.Urls.Add(new SitemapUrl
            {
                ChangeFreq = "daily",
                LastMod = DateTime.UtcNow,
                Loc = "https://blog.laobian.me",
                Priority = "1.0"
            });

            sitemap.Urls.Add(new SitemapUrl
            {
                ChangeFreq = "weekly",
                LastMod = DateTime.UtcNow,
                Loc = "https://blog.laobian.me/category",
                Priority = "0.9"
            });

            sitemap.Urls.Add(new SitemapUrl
            {
                ChangeFreq = "weekly",
                LastMod = DateTime.UtcNow,
                Loc = "https://blog.laobian.me/archive",
                Priority = "0.9"
            });

            sitemap.Urls.Add(new SitemapUrl
            {
                ChangeFreq = "monthly",
                LastMod = DateTime.UtcNow,
                Loc = "https://blog.laobian.me/about",
                Priority = "0.9"
            });

            sitemap.Urls.Add(new SitemapUrl
            {
                ChangeFreq = "daily",
                LastMod = DateTime.UtcNow,
                Loc = "https://blog.laobian.me/rss",
                Priority = "0.9"
            });

            var results = await FindAllPostAsync(true);
            results = results.OrderByDescending(_ => _.CreateAt).ToList();
            foreach (var blogPost in results)
                sitemap.Urls.Add(new SitemapUrl
                {
                    ChangeFreq = "weekly",
                    LastMod = DateTime.UtcNow,
                    Loc = $"https://blog.laobian.me{blogPost.GetFullUrl()}",
                    Priority = "0.8"
                });

            return SerializeHelper.SerializeToXml(sitemap);
        }

        public async Task ExpireAllRandomPostsKeysAsync()
        {
            var keys = await _cacheClient.SetMembersAsync(RandomPostGenerator.KeysCacheKey);
            foreach (var key in keys)
            {
                await _cacheClient.KeyDeleteAsync(key);
            }
        }
    }
}