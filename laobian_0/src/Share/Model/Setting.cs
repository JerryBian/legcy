using Newtonsoft.Json;
using ProtoBuf;

namespace Laobian.Share.Model
{
    [ProtoContract(SkipConstructor = true)]
    public class Setting : BlobDataBase
    {
        [JsonProperty("key")]
        [ProtoMember(1)]
        public string Key { get; set; }

        [JsonProperty("value")]
        [ProtoMember(2)]
        public string Value { get; set; }
    }
}