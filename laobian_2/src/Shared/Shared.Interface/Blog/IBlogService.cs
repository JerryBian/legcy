using System.Collections.Generic;
using System.Threading.Tasks;

namespace Laobian.Shared.Interface.Blog
{
    public interface IBlogService
    {
        Task<string> BuildRssAsync();

        Task<string> BuildSitemapAsync();

        string BuildFullUrl(string path);

        string BuildPostUrl(BlogPost post, string lang = "");

        Task<BlogPost> GetBlogPostByIdAsync(int id);

        Task<int> AddBlogPostAsync(BlogPost post);

        Task IncreasePostVisitAsync(int postContentId);

        Task UpdateBlogPostAsync(BlogPost post);

        Task<BlogPost> GetBlogPostByUrlAsync(int year, int month, string url);

        Task<List<BlogPost>> GetBlogPostsByTagAsync(string tag, bool onlyPublic = true, bool desc = true);

        Task<List<BlogPost>> GetBlogPostsAsync(bool onlyPublic = true, bool desc = true);
    }
}
