using Laobian.Infrasture.Entity.User;
using Laobian.Infrastuture.Const;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace Laobian.Infrastuture.Extension
{
    public static class HttpResponseExtension
    {
        public static void SignIn(this HttpResponse response, User user, string domainName, bool secure, string salt)
        {
            response.Cookies.Append(
                AuthConst.CookieName,
                JsonConvert.SerializeObject(new {
                    id = user.Id,
                    updateTime = user.UpdateTime,
                }).AesEncrypt(salt),
                new CookieOptions
                {
                    Domain = domainName,
                    HttpOnly = true,
                    Expires = DateTimeOffset.UtcNow.AddMonths(1),
                    Secure = secure,
                    Path = "/"
                });
        }

        public static string SignOut(this HttpResponse response, string baseUrl, string returnUrl)
        {
            response.Cookies.Delete(AuthConst.CookieName);
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "/";
            }

            var result = $"{baseUrl}/user/login?url={returnUrl}";
            response.Redirect(result, false);
            return result;
        }
    }
}
