using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laobian.Infrastuture.Entity.Blog
{
    [Table("blogPost")]
    public class BlogPost : EntityBase
    {
        [JsonProperty("title")]
        [Column("title", Order = 1, TypeName = "varchar(1024)")]
        public string Title { get; set; }

        [JsonProperty("url")]
        [Column("url", Order = 2, TypeName = "varchar(1024)")]
        public string Url { get; set; }

        [JsonProperty("htmlContentEn")]
        [Column("htmlContentEn", Order = 3, TypeName = "text")]
        public string HtmlContentEn { get; set; }

        [JsonProperty("mdContentEn")]
        [Column("mdContentEn", Order = 4, TypeName = "text")]
        public string MdContentEn { get; set; }
        

        [JsonProperty("htmlContentCh")]
        [Column("htmlContentCh", Order = 5, TypeName = "text")]
        public string HtmlContentCh { get; set; }

        [JsonProperty("mdContentCh")]
        [Column("mdContentCh", Order = 6, TypeName = "text")]
        public string MdContentCh { get; set; }

        [JsonProperty("publish")]
        [Column("publish", Order = 7, TypeName = "bit(1)")]
        public bool Publish { get; set; }

        [JsonProperty("visits")]
        [NotMapped]
        public long Visits { get; set; }

        [NotMapped]
        [JsonProperty("fullUrl")]
        public string FullUrl
        {
            get {
                return $"{CreateTime.Year}/{CreateTime.ToString("MM")}/{Url}.html";
            }
        }

        [JsonProperty("tags")]
        [Column("tags", Order = 8, TypeName = "varchar(1024)")]
        public string Tags { get; set; }
    }
}
