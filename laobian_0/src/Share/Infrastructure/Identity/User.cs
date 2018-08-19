using Laobian.Share.Utility.Helper;
using Newtonsoft.Json;

namespace Laobian.Share.Infrastructure.Identity
{
    public class User
    {
        [JsonProperty("full_name")] public string FullName { get; set; }

        [JsonProperty("password")] public string Password { get; set; }

        [JsonProperty("email")] public string Email { get; set; }

        // the hash is used to verify underlying user properties
        // any of them changed, we should revoke the token for upperstream, e.g. authentication
        public string GetHashKey()
        {
            return SerializeHelper.SerializeToJson(new
            {
                email = Email.ToLowerInvariant(),
                password = Password,
                fullName = FullName.ToLowerInvariant()
            });
        }
    }
}