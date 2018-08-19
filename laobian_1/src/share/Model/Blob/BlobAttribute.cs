using System;
using Newtonsoft.Json;

namespace Laobian.Share.Model.Blob
{
    public class BlobAttribute
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("create")]
        public DateTimeOffset? Create { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("base_type")]
        public string BaseType { get; set; }

        [JsonProperty("image_html")]
        public string ImageHtml { get; set; }
    }
}
