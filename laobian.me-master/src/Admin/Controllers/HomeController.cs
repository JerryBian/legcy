using Microsoft.AspNetCore.Mvc;
using Laobian.Infrastuture.Model;
using Microsoft.Extensions.Options;

namespace Laobian.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppSettings _settings;

        public HomeController(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
