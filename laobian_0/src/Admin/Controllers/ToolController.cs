using System.IO;
using System.Threading.Tasks;
using Laobian.Share.Utility.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Laobian.Admin.Controllers
{
    [Route("tool")]
    public class ToolController : Controller
    {
        [HttpGet]
        [Route("bcrypt")]
        public IActionResult BCryptHash()
        {
            return View();
        }

        [HttpPost]
        [Route("bcrypt")]
        public async Task<string> GenerateBCryptHash()
        {
            var hashKey = await new StreamReader(Request.Body).ReadToEndAsync();
            return BCryptHelper.HashString(hashKey);
        }
    }
}