using Laobian.Infrastuture.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Laobian.Api.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Ok";
        }
    }
}
