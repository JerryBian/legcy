using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Laobian.Admin.Models;
using Laobian.Share.Infrastructure.Config;
using Laobian.Share.Infrastructure.Identity;
using Laobian.Share.Infrastructure.Mvc.Head;
using Laobian.Share.Utility.Extension;
using Laobian.Share.Utility.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Laobian.Admin.Controllers
{
    [Authorize]
    [Route("identity")]
    public class IdentityController : Controller
    {
        private readonly ConfigSetting _configSetting;

        public IdentityController(ConfigSetting configSetting)
        {
            _configSetting = configSetting;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromQuery] string r)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (string.IsNullOrEmpty(r)) r = "/";

            ViewData["ReturnUrl"] = r;

            var headItem = (HeadItem) HttpContext.Items[HeadItem.HeadItemKey];
            headItem.PageTitle = "Login";
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] IdentityViewModel identity, [FromQuery] string r)
        {
            ViewData["ReturnUrl"] = r;
            if (ModelState.IsValid)
            {
                if (!identity.Email.EqualIgnoreCase(_configSetting.AdminEmail) ||
                    identity.Password != _configSetting.AdminPassword)
                {
                    ModelState.AddModelError(string.Empty, "Ivalid login attempt.");
                    return View(identity);
                }

                var claims = new List<Claim>
                {
                    new Claim(IdentityConstant.ClaimTypeHash,
                        BCryptHelper.HashString(_configSetting.AdminUser.GetHashKey()))
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties
                    {
                        IsPersistent = true
                    });

                return LocalRedirect(r);
            }

            return View(identity);
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}