using System.Threading.Tasks;
using Laobian.Share.Infrastructure.Config;
using Laobian.Share.Infrastructure.Identity;
using Laobian.Share.Utility.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Laobian.Share.Infrastructure.Mvc
{
    public class AuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly ConfigSetting _configSetting;

        public AuthenticationEvents(ConfigSetting configSetting)
        {
            _configSetting = configSetting;
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var hash = ClaimsExtractor.GetHash(context.Principal.Claims);
            if (!string.IsNullOrEmpty(hash))
                if (BCryptHelper.VerifyHash(_configSetting.AdminUser.GetHashKey(), hash))
                {
                    context.HttpContext.Items[IdentityConstant.ItemKey] = _configSetting.AdminUser;
                    return;
                }

            context.RejectPrincipal();
            await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}