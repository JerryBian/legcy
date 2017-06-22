using Laobian.Infrasture.Entity.User;
using Laobian.Infrastuture.Const;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Laobian.Infrastuture.Extension
{
    public static class HttpRequestExtension
    {
        public static User ExtractUser(this HttpRequest request, string salt)
        {
            if (!request.Cookies.ContainsKey(AuthConst.CookieName) || string.IsNullOrEmpty(request.Cookies[AuthConst.CookieName]))
            {
                return null;
            }

            var user = request.Cookies[AuthConst.CookieName].AesDecrypt(salt);
            try
            {
                return JsonConvert.DeserializeObject<User>(user);
            }
            catch
            {
                return null;
            }
        }
    }
}
