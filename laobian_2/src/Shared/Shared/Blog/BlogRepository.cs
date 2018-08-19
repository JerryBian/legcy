using Laobian.Shared.Interface.Blog;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Linq;
using Laobian.Shared.Interface.Options;
using Microsoft.Extensions.Options;

namespace Laobian.Shared.Blog
{
    public class BlogRepository : IBlogRepository
    {
        private readonly string _connectionString;

        public BlogRepository(IOptions<ConfigurationBase> config)
        {
            _connectionString = config.Value.DbConnectionString;
        }

        public async Task IncreasePostVisitAsync(int postContentId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync("usp_increase_post_visit", new {postContentId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<BlogPost> GetPostByUrlAsync(int year, int month, string url)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var result = await connection.QueryMultipleAsync("usp_get_post_by_url", param: new {year,  month,  url }, commandType: CommandType.StoredProcedure))
                {
                    var post = await result.ReadFirstOrDefaultAsync<BlogPost>();

                    if (post != null)
                    {
                        post.Contents = (await result.ReadAsync<BlogPostContent>()).AsList();
                        post.Tags.Clear();
                        post.Tags.AddRange(await result.ReadAsync<BlogTag>());
                    }

                    return post;
                }
            }
        }

        public async Task<BlogPost> GetPostByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var result = await connection.QueryMultipleAsync("usp_get_post_by_id", param: new {  id}, commandType: CommandType.StoredProcedure))
                {
                    var post = (await result.ReadAsync<BlogPost>()).FirstOrDefault();

                    if (post != null)
                    {
                        post.Contents = (await result.ReadAsync<BlogPostContent>()).AsList();
                        post.Tags.Clear();
                        post.Tags.AddRange(await result.ReadAsync<BlogTag>());
                    }

                    return post;
                }
            }
        }

        public async Task<List<BlogPost>> GetPostsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var result = await connection.QueryMultipleAsync("usp_get_posts", commandType: CommandType.StoredProcedure))
                {
                    var posts = (await result.ReadAsync<BlogPost>()).AsList();
                    var postContents = (await result.ReadAsync<BlogPostContent>()).AsList();
                    var postTags = (await result.ReadAsync<BlogTag>()).AsList();

                    foreach (var post in posts)
                    {
                        post.Contents = postContents.Where(_ => _.PostId == post.Id).AsList();
                        post.Tags.Clear();
                        post.Tags.AddRange(postTags.Where(_ => _.PostId == post.Id));
                    }

                    return posts;
                }

            }
        }

        public async Task<List<BlogPost>> GetPostsByTagAsync(string tag)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var result = await connection.QueryMultipleAsync("usp_get_posts_by_tag", param: new {  tag}, commandType: CommandType.StoredProcedure))
                {
                    var posts = (await result.ReadAsync<BlogPost>()).AsList();
                    var postContents = (await result.ReadAsync<BlogPostContent>()).AsList();
                    var postTags = (await result.ReadAsync<BlogTag>()).AsList();

                    foreach (var post in posts)
                    {
                        post.Contents = postContents.Where(_ => _.PostId == post.Id).AsList();
                        post.Tags.Clear();
                        post.Tags.AddRange(postTags.Where(_ => _.PostId == post.Id));
                    }

                    return posts;
                }

            }
        }

        public async Task<int> AddPostAsync(BlogPost post)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("markdown", typeof(string));
            dataTable.Columns.Add("html", typeof(string));
            dataTable.Columns.Add("title", typeof(string));
            dataTable.Columns.Add("isPublic", typeof(bool));
            dataTable.Columns.Add("isEnglish", typeof(bool));


            foreach (var item in post.Contents)
            {
                var row = dataTable.NewRow();
                row["markdown"] = item.Markdown;
                row["html"] = item.Html;
                row["isPublic"] = item.IsPublic;
                row["isEnglish"] = item.IsEnglish;
                row["title"] = item.Title;
                dataTable.Rows.Add(row);
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@url", post.Url);
                dynamicParameters.Add("@allowComment", post.AllowComment);
                dynamicParameters.Add("@tags", post.TagsString);
                dynamicParameters.Add("@postContent", dataTable.AsTableValuedParameter());
                dynamicParameters.Add("@postId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await connection.ExecuteAsync(
                    "usp_add_post",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
                return dynamicParameters.Get<int>("postId");
            }
        }

        public async Task UpdatePostAsync(BlogPost post)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("markdown", typeof(string));
            dataTable.Columns.Add("html", typeof(string));
            dataTable.Columns.Add("title", typeof(string));
            dataTable.Columns.Add("isPublic", typeof(bool));
            dataTable.Columns.Add("isEnglish", typeof(bool));

            foreach (var item in post.Contents)
            {
                var row = dataTable.NewRow();
                row["markdown"] = item.Markdown;
                row["html"] = item.Html;
                row["isPublic"] = item.IsPublic;
                row["isEnglish"] = item.IsEnglish;
                row["title"] = item.Title;
                dataTable.Rows.Add(row);
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@id", post.Id);
                dynamicParameters.Add("@url", post.Url);
                dynamicParameters.Add("@allowComment", post.AllowComment);
                dynamicParameters.Add("@tags", post.TagsString);
                dynamicParameters.Add("@postContent", dataTable.AsTableValuedParameter());

                await connection.ExecuteAsync(
                    "usp_update_post",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
