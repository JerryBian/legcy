using Laobian.Infrastuture.Entity.Blog;
using Laobian.Infrastuture.Interface.Repository;
using Laobian.Infrastuture.Model;
using Microsoft.Extensions.Options;

namespace Laobian.Repository
{
    public class BlogPostRepository : RepositoryBase<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(IOptions<AppSettings> setting) : base(setting)
        {
            
        }
    }
}
