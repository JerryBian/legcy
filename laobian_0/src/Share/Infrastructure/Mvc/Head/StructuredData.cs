using System.Collections.Generic;
using Newtonsoft.Json;

namespace Laobian.Share.Infrastructure.Mvc.Head
{
    public class StructuredData
    {
        [JsonProperty("@context")] public string Context { get; set; } = "http://schema.org";

        [JsonProperty("@type")] public string Type { get; set; } = "NewsArticle";

        [JsonProperty("mainEntityOfPage")]
        public MainEntityOfPage MainEntityOfPage { get; set; }

        [JsonProperty("headline")]
        public string Headline { get; set; }

        [JsonProperty("image")]
        public List<string> Image { get; set; }

        [JsonProperty("datePublished")]
        public string DatePublished { get; set; }

        [JsonProperty("dateModified")]
        public string DateModified { get; set; }

        [JsonProperty("author")]
        public Author Author { get; set; }

        [JsonProperty("publisher")]
        public Publisher Publisher { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class MainEntityOfPage
    {
        [JsonProperty("@type")]
        public string Type { get; set; } = "WebPage";

        [JsonProperty("@id")]
        public string Id { get; set; }
    }

    public class Author
    {
        [JsonProperty("@type")] public string Type { get; set; } = "Person";

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Publisher
    {
        [JsonProperty("@type")] public string Type { get; set; } = "Organization";

        [JsonProperty("name")] public string Name { get; set; } = "卞良忠";

        [JsonProperty("logo")]
        public Logo Logo { get; set; }
    }

    public class Logo
    {
        [JsonProperty("@type")] public string Type { get; set; } = "ImageObject";

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
