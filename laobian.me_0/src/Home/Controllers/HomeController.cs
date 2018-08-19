using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Laobian.Infrastuture.Model;
using System.Threading.Tasks;
using Laobian.Infrastuture.Interface.Service;
using Laobian.Infrastuture.Extension;
using Laobian.Infrastuture.Const;

namespace Home.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppSettings _settings;
        private readonly IUserService _userService;

        public HomeController(IOptions<AppSettings> settings, IUserService userService)
        {
            _settings = settings.Value;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var user = HttpContext.Request.ExtractUser(_settings.Salt);
            if(user != null)
            {
                var record = await _userService.FindAsync(user.Id);
                if (record != null && user.Role == UserRole.Admin.ToString() || user.UpdateTime == user.UpdateTime)
                {
                    return View();
                }
            }

            return Redirect("/user/login");
        }
    }
}
