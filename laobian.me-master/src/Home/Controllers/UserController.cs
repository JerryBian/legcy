using Laobian.Infrasture.Entity.User;
using Laobian.Infrastuture.Const;
using Laobian.Infrastuture.Extension;
using Laobian.Infrastuture.Interface.Service;
using Laobian.Infrastuture.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Laobian.Home.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        private readonly AppSettings _settings;
        private readonly IUserService _userService;
        private readonly IHostingEnvironment _env;

        public UserController(IOptions<AppSettings> settings, IUserService userService, IHostingEnvironment env)
        {
            _env = env;
            _userService = userService;
            _settings = settings.Value;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(User user)
        {
            var record = await _userService.ValidateUserAsync(user.UserName, user.Password);
            if (record == null)
            {
                return Unauthorized();
            }

            var domainName = ".laobian.me";
            var secure = true;
            if (_env.IsDevelopment())
            {
                domainName = "localhost";
                secure = false;
            }

            HttpContext.Response.SignIn(record, domainName, secure, _settings.Salt);
            return Ok();
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login(string url)
        {
            url = string.IsNullOrEmpty(url) ? "/" : url;
            ViewBag.ReturnUrl = url;
            return View();
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout(string url)
        {
            url = string.IsNullOrEmpty(url) ? _settings.BlogHost : url;
            HttpContext.Response.Cookies.Delete(AuthConst.CookieName);
            return Redirect($"{_settings.HomeHost}/user/login?url={url}");
        }
    }
}
