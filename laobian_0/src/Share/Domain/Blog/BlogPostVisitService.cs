using System;
using System.Linq;
using System.Threading.Tasks;
using Laobian.Share.Domain.Interface;
using Laobian.Share.Infrastructure.Interface;
using Laobian.Share.Model;

namespace Laobian.Share.Domain.Blog
{
    public class BlogPostVisitService : IBlogPostVisitService
    {
        private const string KeyPrefix = "__blog_post_visit";
        private readonly IBlobDataRepository<BlogPost> _blogPostRepository;
        private readonly IBlobDataRepository<BlogPostVisit> _blogPostVisitRepository;
        private readonly ICacheClient _cacheClient;

        public BlogPostVisitService(
            ICacheClient cacheClient,
            IBlobDataRepository<BlogPost> blogPostRepository,
            IBlobDataRepository<BlogPostVisit> blogPostVisitRepository)
        {
            _cacheClient = cacheClient;
            _blogPostRepository = blogPostRepository;
            _blogPostVisitRepository = blogPostVisitRepository;
        }

        public async Task AddAsync(Guid postId)
        {
            await _cacheClient.StringIncrementAsync(BuildCacheKey(postId));
        }

        public async Task GetOrResetAsync()
        {
            var posts = await _blogPostRepository.FindAllAsync();
            var postVisits = await _blogPostVisitRepository.FindAllAsync();

            foreach (var p in posts.Select(p => p.Id))
            {
                var value = await _cacheClient.StringGetSetAsync(BuildCacheKey(p), 0);
                if (value > 0)
                {
                    var postVisit = postVisits.FirstOrDefault(pv => pv.PostId == p);

                    if (postVisit == null)
                    {
                        await _blogPostVisitRepository.AddAsync(new BlogPostVisit {PostId = p, Visit = value});
                    }
                    else
                    {
                        postVisit.Visit += value;
                        await _blogPostVisitRepository.UpdateAsync(postVisit);
                    }
                }
            }
        }

        private string BuildCacheKey(Guid postId)
        {
            return $"{KeyPrefix}_{postId}";
        }
    }
}