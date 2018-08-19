using System.Collections.Generic;
using System.Threading.Tasks;

namespace Laobian.Shared.Interface.Blog
{
    public interface IBlogRepository
    {
        Task IncreasePostVisitAsync(int postContentId);

        Task<BlogPost> GetPostByUrlAsync(int year, int month, string url);

        Task<BlogPost> GetPostByIdAsync(int id);

        Task<List<BlogPost>> GetPostsAsync();

        Task<List<BlogPost>> GetPostsByTagAsync(string tag);

        Task<int> AddPostAsync(BlogPost post);

        Task UpdatePostAsync(BlogPost post);
    }
}
