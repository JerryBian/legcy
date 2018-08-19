using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Laobian.Share.Model;

namespace Laobian.Share.Domain.Interface
{
    public interface IBlogService
    {
        Task<List<BlogCategory>> FindAllCategoryAsync();

        Task AddCategoryAsync(BlogCategory blogCategory);

        Task UpdateCategoryAsync(BlogCategory blogCategoryEntity);

        Task<BlogCategory> FindCategoryAsync(Guid id);

        Task<List<BlogPost>> FindAllPostAsync(bool? isPublish = null);

        Task<BlogPost> FindPostAsync(Guid id);

        Task UpdatePostAsync(BlogPost blogPost);

        Task<BlogPost> FindPostAsync(int year, int month, string url);

        Task AddPostVisitAsync(Guid postId);

        Task AddPostVisitAsync(int year, int month, string url);

        Task AddPostAsync(BlogPost blogPost);

        Task<List<BlogPost>> FindAllByCategoryAsync(string categoryUrl, bool? isPublish = null);

        Task<string> BuildSitemapAsync();

        Task<string> BuildRssAsync();
    }
}