using System;
using Newtonsoft.Json;

namespace Laobian.Share.Model.Blog
{
    public class BlogPost
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("excerpt")]
        public string Excerpt { get; set; }

        [JsonProperty("create_time")]
        public DateTime CreateTime { get; set; }

        [JsonProperty("update_time")]
        public DateTime UpdateTime { get; set; }

        [JsonProperty("content_md")]
        public string ContentMd { get; set; }

        [JsonProperty("content_html")]
        public string ContentHtml { get; set; }

        [JsonProperty("is_publish")]
        public bool IsPublish { get; set; }

        [JsonProperty("category")]
        public BlogCategory Category { get; set; }

        public string GetFullUrl()
        {
            return $"/{CreateTime.Year:0000}/{CreateTime.Month:00}/{Url}.html";
        }

        public string GetCategoryLink()
        {
            if (Category == null)
            {
                return "<span>No Category</span>";
            }

            return $"<a href='{Category.GetUrl()}'>{Category.Name}</a>";
        }
    }
}
