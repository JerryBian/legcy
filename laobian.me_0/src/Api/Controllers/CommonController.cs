using Laobian.Infrastuture.Interface.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace Laobian.Api.Controllers
{
    public class CommonController : Controller
    {
        private readonly ILaobianAzureStorage _azureStorage;

        public CommonController(ILaobianAzureStorage azureStorage)
        {
            _azureStorage = azureStorage;
        }

        [HttpPost]
        [Route("/file/upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var result = await _azureStorage.UploadFileAsync(file.FileName, memoryStream.ToArray());
                return Ok(result);
            }
        }
    }
}
