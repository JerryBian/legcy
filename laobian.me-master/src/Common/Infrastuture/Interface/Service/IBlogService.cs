using Laobian.Infrastuture.Entity.Blog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Laobian.Infrastuture.Interface.Service
{
    public interface IBlogService
    {
        Task AddPostAsync(BlogPost post);

        Task<List<BlogPost>> GetAllPostsAsync(bool onlyPublished = true);

        Task DeletePostAsync(int id);

        Task<BlogPost> GetPostAsync(int id);

        Task UpdatePostAsync(BlogPost post);

        Task<BlogPost> GetPostByUrl(string url);

        Task<List<BlogPost>> GetPostsByTag(string tag);
    }
}
