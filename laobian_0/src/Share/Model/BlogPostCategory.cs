using System;
using Newtonsoft.Json;
using ProtoBuf;

namespace Laobian.Share.Model
{
    [ProtoContract(SkipConstructor = true)]
    public class BlogPostCategory : BlobDataBase
    {
        [JsonProperty("post_id")]
        [ProtoMember(1)]
        public Guid PostId { get; set; }

        [JsonProperty("category_id")]
        [ProtoMember(2)]
        public Guid CategoryId { get; set; }
    }
}