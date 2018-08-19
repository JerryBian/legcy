using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Laobian.Share.Model;
using Laobian.Share.Model.Blog;
using MySql.Data.MySqlClient;

namespace Laobian.Share.Data.Repository
{
    public interface IBlogRepository
    {
        Task<IEnumerable<BlogCategory>> FindAllCategooriesAsync();

        Task<BlogCategory> FindCategoryAsync(int id);

        Task AddCategoryAsync(BlogCategory category);

        Task UpdateCategoryAsync(BlogCategory category);

        Task DeleteCategoryAsync(int id);

        Task<List<BlogPost>> FindAllPostsAsync();

        Task<BlogPost> FindPostAsync(int id);

        Task<BlogPost> FindPostByUrlAsync(int year, int month, string url);

        Task<List<BlogPost>> FindPostsByDateAsync(int year, int? month = null);

        Task<List<BlogPost>> FindPostsByCategoryAsync(string url);

        Task AddPostAsync(BlogPost post);

        Task UpdatePostAsync(BlogPost post);

        Task TogglePostPublishAsync(int postId);
    }

    public class BlogRepository : IBlogRepository
    {
        private readonly string _connectionString;

        public BlogRepository(Config config)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            _connectionString = config.MySqlDatabaseConnection;
        }

        public async Task<IEnumerable<BlogCategory>> FindAllCategooriesAsync()
        {
            using (var mysqlConnection = new MySqlConnection(_connectionString))
            {
                return await mysqlConnection.QueryAsync<BlogCategory>(
                    "get_blog_category",
                    new { category_id = (int?)null },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<BlogCategory> FindCategoryAsync(int id)
        {
            using (var mysqlConnection = new MySqlConnection(_connectionString))
            {
                return await mysqlConnection.QueryFirstOrDefaultAsync<BlogCategory>(
                    "get_blog_category",
                    new { category_id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task AddCategoryAsync(BlogCategory category)
        {
            using (var mysqlConnection = new MySqlConnection(_connectionString))
            {
                await mysqlConnection.ExecuteAsync("insert_blog_category",
                    new { category_name = category.Name, category_url = category.Url }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateCategoryAsync(BlogCategory category)
        {
            using (var mysqlConnection = new MySqlConnection(_connectionString))
            {
                await mysqlConnection.ExecuteAsync("update_blog_category",
                    new { category_id = category.Id, category_name = category.Name, category_url = category.Url },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            using (var mysqlConnection = new MySqlConnection(_connectionString))
            {
                await mysqlConnection.ExecuteAsync("delete_blog_category",
                    new { category_Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<BlogPost>> FindAllPostsAsync()
        {
            using (var mysqlConnection = new MySqlConnection(_connectionString))
            {
                var result = await mysqlConnection.QueryAsync<BlogPost, BlogCategory, BlogPost>("get_blog_post",
                    (post, category) =>
                    {
                        post.Category = category;
                        return post;
                    },
                    param: new { post_id = (int?)null },
                    commandType: CommandType.StoredProcedure);

                return result.AsList();
            }
        }

        public async Task<BlogPost> FindPostAsync(int id)
        {
            using (var mysqlConnection = new MySqlConnection(_connectionString))
            {
                var posts = await mysqlConnection.QueryAsync<BlogPost, BlogCategory, BlogPost>(
                    "get_blog_post",
                    (post, category) =>
                    {
                        post.Category = category;
                        return post;
                    },
                    new { post_id = id },
                    commandType: CommandType.StoredProcedure);
                return posts.FirstOrDefault();
            }
        }

        public async Task<BlogPost> FindPostByUrlAsync(int year, int month, string url)
        {
            using (var mysqlConnection = new MySqlConnection(_connectionString))
            {
                var posts = await mysqlConnection.QueryAsync<BlogPost, BlogCategory, BlogPost>(
                    "get_blog_post_by_url",
                    (post, category) =>
                    {
                        post.Category = category;
                        return post;
                    },
                    new
                    {
                        year = year,
                        month = month,
                        post_url = url
                    },
                    commandType: CommandType.StoredProcedure);
                return posts.FirstOrDefault();
            }
        }

        public async Task<List<BlogPost>> FindPostsByDateAsync(int year, int? month = null)
        {
            using (var mysqlConnection = new MySqlConnection(_connectionString))
            {
                var posts = await mysqlConnection.QueryAsync<BlogPost>(
                    "get_blog_post_by_date",
                    new
                    {
                        year = year,
                        month = month
                    },
                    commandType: CommandType.StoredProcedure);
                return posts.AsList();
            }
        }

        public async Task<List<BlogPost>> FindPostsByCategoryAsync(string url)
        {
            using (var mysqlConnection = new MySqlConnection(_connectionString))
            {
                var posts = await mysqlConnection.QueryAsync<BlogPost, BlogCategory, BlogPost>(
                    "get_blog_post_by_category",
                    (post, category) =>
                    {
                        post.Category = category;
                        return post;
                    },
                    new
                    {
                        category_url = url
                    },
                    commandType: CommandType.StoredProcedure);
                return posts.AsList();
            }
        }

        public async Task AddPostAsync(BlogPost post)
        {
            using (var mysqlConnection = new MySqlConnection(_connectionString))
            {
                await mysqlConnection.ExecuteAsync(
                    "insert_blog_post",
                    new
                    {
                        post_title = post.Title,
                        post_url = post.Url,
                        post_excerpt = post.Excerpt,
                        post_content_md = post.ContentMd,
                        post_content_html = post.ContentHtml,
                        post_is_publish = post.IsPublish,
                        post_category_id = post.Category?.Id
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdatePostAsync(BlogPost post)
        {
            using (var mysqlConnection = new MySqlConnection(_connectionString))
            {
                await mysqlConnection.ExecuteAsync(
                    "update_blog_post",
                    new
                    {
                        post_id = post.Id,
                        post_title = post.Title,
                        post_url = post.Url,
                        post_excerpt = post.Excerpt,
                        post_content_md = post.ContentMd,
                        post_content_html = post.ContentHtml,
                        post_is_publish = post.IsPublish,
                        post_category_id = post.Category?.Id
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task TogglePostPublishAsync(int postId)
        {
            using (var mysqlConnection = new MySqlConnection(_connectionString))
            {
                await mysqlConnection.ExecuteAsync(
                    "toggle_blog_post_publish",
                    new
                    {
                        post_id = postId
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
