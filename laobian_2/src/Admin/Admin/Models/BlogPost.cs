using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laobian.Admin.Models
{
    public class BlogPost
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("allowComment")]
        public bool AllowComment { get; set; }

        [JsonProperty("title_en")]
        public string TitleEn { get; set; }

        [JsonProperty("title_zh")]
        public string TitleZh { get; set; }

        [JsonProperty("md_en")]
        public string MdEn { get; set; }

        [JsonProperty("md_zh")]
        public string MdZh { get; set; }

        [JsonProperty("isPublic_en")]
        public bool IsPublicEn { get; set; }

        [JsonProperty("isPublic_zh")]
        public bool IsPublicZh { get; set; }

        [JsonProperty("tags")]
        public string Tags { get; set; }
    }
}
