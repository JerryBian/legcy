using System;
using Newtonsoft.Json;

namespace Laobian.Share.Model.Blog
{
    public class BlogCategory
    {
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        public string GetUrl()
        {
            return $"/category/{Url}";
        }
    }
}

