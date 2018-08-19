using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Laobian.Share.Domain.Interface;
using Laobian.Share.Domain.Setting;
using Laobian.Share.Infrastructure.Cache;
using Laobian.Share.Infrastructure.Interface;
using Laobian.Share.Model;
using Laobian.Share.Utility.Helper;

namespace Laobian.Share.Domain.Blog.RandomPost
{
    public class RandomPostGenerator : IRandomPostGenerator
    {
        public const string KeysCacheKey = "__Random_Posts__All_Keys";
        private readonly IBlogService _blogService;
        private readonly ICacheClient _cacheClient;
        private readonly Random _random;
        private readonly ISettingService _settingService;

        public RandomPostGenerator(
            IBlogService blogService, 
            ISettingService settingService, 
            ICacheClient cacheClient)
        {
            _random = new Random();
            _blogService = blogService;
            _cacheClient = cacheClient;
            _settingService = settingService;
        }

        public async Task<List<BlogPost>> Execute(RandomPostFactor factor)
        {
            var cacheKey = $"__Random_Posts_{SerializeHelper.SerializeToJson(factor)}";
            var cacheItem = await _cacheClient.StringGetAsync<List<BlogPost>>(cacheKey);
            if (cacheItem == null)
            {
                try
                {
                    await _cacheClient.SetAddAsync(KeysCacheKey, cacheKey); // store the key to be poped up later
                    var maxCount = await _settingService.FindAsync<int>(SettingKey.BlogPostRandomCount);
                    var posts = (await _blogService.FindAllPostAsync()).ToList();
                    var result = new List<BlogPost>();

                    if (factor.ExcludePosts != null && factor.ExcludePosts.Any())
                        posts = posts.Where(_ => !factor.ExcludePosts.Contains(_.Id)).ToList();

                    posts = factor.AdminView ? posts.Where(_ => !_.IsPrivate).ToList() : posts.Where(_ => _.IsPublish).ToList();

                    if (factor.AlikePost != Guid.Empty)
                    {
                        var alikePost = posts.FirstOrDefault(_ => _.Id == factor.AlikePost);
                        if (alikePost != null)
                        {
                            posts.Remove(alikePost);
                            foreach (var cat in alikePost.BlogCategories)
                            {
                                var sameCatPosts = posts.Where(_ => _.BlogCategories.Any(c => c.Id == cat.Id)).ToList();
                                var post = GetRandomPosts(sameCatPosts, 1);
                                result.AddRange(post);
                                posts.RemoveAll(_ => post.Any(p => p.Id == _.Id));
                            }

                            if (result.Count() < maxCount)
                            {
                                var sameMonthPosts = posts.Where(_ => _.CreateAt.Month == alikePost.CreateAt.Month).ToList();
                                var toBeAdded = GetRandomPosts(sameMonthPosts, maxCount - result.Count);
                                result.AddRange(toBeAdded);
                                posts.RemoveAll(_ => toBeAdded.Any(p => p.Id == _.Id));
                            }
                        }
                    }

                    var lastPosts = GetRandomPosts(posts, maxCount - result.Count);
                    result.AddRange(lastPosts);

                    cacheItem = result.OrderBy(_ => _random.Next()).ToList();
                    await _cacheClient.StringSetAsync(cacheKey, cacheItem, TimeSpan.FromDays(1));
                }
                finally
                {
                }
            }

            return cacheItem;
        }

        

        private List<BlogPost> GetRandomPosts(List<BlogPost> posts, int count)
        {
            var result = new List<BlogPost>();
            if (count <= 0)
            {
                return result;
            }

            if (posts.Count <= count)
            {
                count = posts.Count;
            }

            var postsClone = new List<BlogPost>(posts);
            for (var i = 0; i < count; i++)
            {
                var post = postsClone[_random.Next(0, postsClone.Count - 1)];
                result.Add(post);
                postsClone.Remove(post);
            }


            return result;
        }
    }
}