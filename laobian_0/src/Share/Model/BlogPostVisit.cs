using System;
using Newtonsoft.Json;
using ProtoBuf;

namespace Laobian.Share.Model
{
    [ProtoContract(SkipConstructor = true)]
    public class BlogPostVisit : BlobDataBase
    {
        [JsonProperty("post_id")]
        [ProtoMember(1)]
        public Guid PostId { get; set; }

        [JsonProperty("visit")]
        [ProtoMember(2)]
        public int Visit { get; set; }
    }
}