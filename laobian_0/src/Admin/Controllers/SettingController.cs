using System.Threading.Tasks;
using Laobian.Share.Domain.Interface;
using Laobian.Share.Domain.Setting;
using Laobian.Share.Infrastructure.Mvc.Head;
using Laobian.Share.Model;
using Microsoft.AspNetCore.Mvc;

namespace Laobian.Admin.Controllers
{
    [Route("setting")]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _settingService.FindAllAsync();

            var headItem = (HeadItem) HttpContext.Items[HeadItem.HeadItemKey];
            headItem.PageTitle = "All · Setting";
            return View(model);
        }

        [HttpGet]
        [Route("update/{key?}")]
        public async Task<IActionResult> Update([FromRoute] string key)
        {
            var model = await _settingService.FindAsync(key);

            var headItem = (HeadItem) HttpContext.Items[HeadItem.HeadItemKey];
            headItem.PageTitle = "Update · Setting";
            return View("Update", model);
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] Setting setting)
        {
            await _settingService.UpdateAsync(setting);
            return Ok();
        }
    }
}