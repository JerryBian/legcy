using Laobian.Shared.Interface;
using Laobian.Shared.Interface.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Laobian.Shared.Blog
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ISharedService _sharedService;

        public BlogService(
            ISharedService sharedService,
            IBlogRepository blogRepository)
        {
            _sharedService = sharedService;
            _blogRepository = blogRepository;
        }

        public string BuildPostUrl(BlogPost post, string lang = "")
        {
            if (!post.Contents.Any())
            {
                return "/";
            }

            var postDate = post.Contents.Min(_ => _.CreateTime);
            var result = $"/{postDate.Year:0000}/{postDate.Month:00}/{post.Url}.html";

            return !BlogLang.IsEnglish(lang) ? $"{result}?lang={BlogLang.Chinese}" : result;
        }

        public async Task<int> AddBlogPostAsync(BlogPost post)
        {
            foreach (var item in post.Contents)
            {
                item.Html = CommonMark.CommonMarkConverter.Convert(item.Markdown);
            }

            return await _blogRepository.AddPostAsync(post);
        }

        public async Task<BlogPost> GetBlogPostByIdAsync(int id)
        {
            return await _blogRepository.GetPostByIdAsync(id);
        }

        public async Task<BlogPost> GetBlogPostByUrlAsync(int year, int month, string url)
        {
            return await _blogRepository.GetPostByUrlAsync(year, month, url);
        }

        public async Task<List<BlogPost>> GetBlogPostsByTagAsync(string tag, bool onlyPublic = true, bool desc = true)
        {
            var posts = await _blogRepository.GetPostsByTagAsync(tag);
            var result = new List<BlogPost>();
            if (onlyPublic)
            {
                foreach (var post in posts)
                {
                    post.Contents = post.Contents.Where(_ => _.IsPublic).ToList();
                    if (post.Contents.Any())
                    {
                        result.Add(post);
                    }
                }
            }
            else
            {
                result = posts.Where(_ => _.Contents.Any()).ToList();
            }

            return desc ?
                result.OrderByDescending(_ => _.Contents.Min(c => c.CreateTime)).ToList() :
                result.OrderBy(_ => _.Contents.Min(c => c.CreateTime)).ToList();
        }

        public async Task<List<BlogPost>> GetBlogPostsAsync(bool onlyPublic = true, bool desc = true)
        {
            var posts = await _blogRepository.GetPostsAsync();
            var result = new List<BlogPost>();
            if (onlyPublic)
            {
                foreach (var post in posts)
                {
                    post.Contents = post.Contents.Where(_ => _.IsPublic).ToList();
                    if (post.Contents.Any())
                    {
                        result.Add(post);
                    }
                }
            }
            else
            {
                result = posts.Where(_ => _.Contents.Any()).ToList();
            }

            return desc ?
                result.OrderByDescending(_ => _.Contents.Min(c => c.CreateTime)).ToList() :
                result.OrderBy(_ => _.Contents.Min(c => c.CreateTime)).ToList();
        }

        public async Task UpdateBlogPostAsync(BlogPost post)
        {
            foreach (var item in post.Contents)
            {
                item.Html = CommonMark.CommonMarkConverter.Convert(item.Markdown);
            }

            await _blogRepository.UpdatePostAsync(post);
        }

        public async Task IncreasePostVisitAsync(int postContentId)
        {
            await _blogRepository.IncreasePostVisitAsync(postContentId);
        }

        public async Task<string> BuildRssAsync()
        {
            var rss = await BuildRssInternalAsync();
            var doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
            var root = new XElement("rss");
            root.SetAttributeValue("version", "2.0");
            var channel = new XElement("channel");
            root.Add(channel);
            foreach (var prop in rss.Channel.GetType().GetProperties())
            {
                if (prop.PropertyType == typeof(string))
                {
                    var ele = new XElement(char.ToLowerInvariant(prop.Name[0]) + prop.Name.Substring(1), prop.GetValue(rss.Channel));
                    channel.Add(ele);
                }

                if (prop.PropertyType == typeof(Image))
                {
                    var img = new XElement("image");
                    foreach (var i in rss.Channel.Image.GetType().GetProperties())
                    {
                        if (i.PropertyType == typeof(string))
                        {
                            var e = new XElement(char.ToLowerInvariant(i.Name[0]) + i.Name.Substring(1), i.GetValue(rss.Channel.Image, null));
                            img.Add(e);
                        }
                    }
                    channel.Add(img);
                }

                if (prop.PropertyType == typeof(List<Item>))
                {
                    foreach (var item in rss.Channel.Items)
                    {
                        var ie = new XElement("item");
                        foreach (var k in item.GetType().GetProperties())
                        {
                            if (k.PropertyType == typeof(string))
                            {
                                var e = new XElement(char.ToLowerInvariant(k.Name[0]) + k.Name.Substring(1), k.GetValue(item));
                                ie.Add(e);
                            }
                        }
                        channel.Add(ie);
                    }
                }
            }
            doc.Add(root);
            return doc.ToString();
        }

        public async Task<string> BuildSitemapAsync()
        {
            var sitemap = await BuildSitemapInternalAsync();
            var doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
            var urlSet = new XElement("urlset");
            //urlSet.SetAttributeValue("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
            foreach (var url in sitemap.Urlset.Urls)
            {
                var e = new XElement("url");
                e.Add(new XElement("loc", url.Loc));
                e.Add(new XElement("lastmod", url.Lastmod));
                e.Add(new XElement("changefreq", url.Changefreq));
                e.Add(new XElement("priority", url.Priority));
                urlSet.Add(e);
            }
            doc.Add(urlSet);
            return doc.ToString();
        }

        private async Task<Rss> BuildRssInternalAsync()
        {
            var results = await GetBlogPostsAsync();
            var rss = new Rss();
            rss.Channel.Title = await _sharedService.GetSettingAsync(SettingKey.BlogDefaultTitle);
            rss.Channel.Copyright = await _sharedService.GetSettingAsync(SettingKey.Copyright);
            rss.Channel.Docs = "http://www.rssboard.org/rss-specification";
            rss.Channel.Language = "en-US";
            rss.Channel.Link = "https://blog.laobian.me";
            rss.Channel.Category = "blog";
            rss.Channel.WebMaster = await _sharedService.GetSettingAsync(SettingKey.Email);
            rss.Channel.ManagingEditor = await _sharedService.GetSettingAsync(SettingKey.Email);
            rss.Channel.Ttl = "60";
            rss.Channel.LastBuildDate = DateTime.UtcNow.ToString("r");
            rss.Channel.Description = await _sharedService.GetSettingAsync(SettingKey.BlogDefaultDescription);
            foreach (var blogPost in results)
            {
                var item = new Item
                {
                    Author = await _sharedService.GetSettingAsync(SettingKey.Author),
                    Guid = BuildPostUrl(blogPost),
                    Link = BuildPostUrl(blogPost),
                    Description = blogPost.GetByLangWithDefault().Html,
                    Title = blogPost.GetByLangWithDefault().Title,
                    PubDate = blogPost.GetByLangWithDefault().CreateTime.ToString("r")
                };

                rss.Channel.Items.Add(item);
            }

            return rss;
        }

        private async Task<Sitemap> BuildSitemapInternalAsync()
        {
            var sitemap = new Sitemap();
            sitemap.Urlset.Urls.Add(new Url
            {
                Changefreq = "daily",
                Lastmod = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Loc = "https://blog.laobian.me",
                Priority = "1.0"
            });

            sitemap.Urlset.Urls.Add(new Url
            {
                Changefreq = "weekly",
                Lastmod = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Loc = "https://blog.laobian.me/tags",
                Priority = "0.9"
            });

            sitemap.Urlset.Urls.Add(new Url
            {
                Changefreq = "weekly",
                Lastmod = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Loc = "https://blog.laobian.me/archives",
                Priority = "0.9"
            });

            sitemap.Urlset.Urls.Add(new Url
            {
                Changefreq = "monthly",
                Lastmod = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Loc = "https://blog.laobian.me/about",
                Priority = "0.9"
            });

            sitemap.Urlset.Urls.Add(new Url
            {
                Changefreq = "daily",
                Lastmod = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Loc = "https://blog.laobian.me/rss",
                Priority = "0.9"
            });

            var results = await GetBlogPostsAsync();
            foreach (var blogPost in results)
            {
                sitemap.Urlset.Urls.Add(new Url
                {
                    Changefreq = "weekly",
                    Lastmod = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                    Loc = $"https://blog.laobian.me{BuildPostUrl(blogPost)}",
                    Priority = "0.8"
                });
            }

            return sitemap;
        }

        public string BuildFullUrl(string path)
        {
            path = string.IsNullOrEmpty(path) ? "/" : path;
            if (!path.StartsWith("/"))
            {
                path = "/" + path;
            }

            return $"{Constants.BlogBaseUrl}{path}";
        }
    }
}
