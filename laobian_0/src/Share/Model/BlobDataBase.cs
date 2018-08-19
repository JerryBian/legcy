using System;
using System.ComponentModel;
using Newtonsoft.Json;
using ProtoBuf;

namespace Laobian.Share.Model
{
    [ProtoInclude(1, typeof(BlogCategory))]
    [ProtoInclude(2, typeof(BlogPost))]
    [ProtoInclude(3, typeof(BlogPostCategory))]
    [ProtoInclude(4, typeof(BlogPostVisit))]
    [ProtoInclude(5, typeof(Setting))]
    public abstract class BlobDataBase
    {
        [JsonProperty("id")]
        [ProtoMember(1)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty("create_at")]
        [ProtoMember(2)]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        [JsonProperty("last_update_at")]
        [ProtoMember(3)]
        public DateTime LastUpdateAt { get; set; } = DateTime.UtcNow;
    }
}