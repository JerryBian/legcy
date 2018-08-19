using Newtonsoft.Json;
using ProtoBuf;

namespace Laobian.Share.Model
{
    [ProtoContract(SkipConstructor = true)]
    public class BlogCategory : BlobDataBase
    {
        [JsonProperty("name")]
        [ProtoMember(1)]
        public string Name { get; set; }

        [JsonProperty("url")]
        [ProtoMember(2)]
        public string Url { get; set; }

        public string GetFullUrl()
        {
            return $"/category/{Url}";
        }
    }
}