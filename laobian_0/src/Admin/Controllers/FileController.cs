using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Laobian.Admin.Models;
using Laobian.Share.Infrastructure.Blob;
using Laobian.Share.Infrastructure.Interface;
using Laobian.Share.Infrastructure.Mvc.Head;
using Laobian.Share.Utility.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Laobian.Admin.Controllers
{
    [Route("file")]
    public class FileController : Controller
    {
        private readonly IBlobClient _blobClient;

        public FileController(IBlobClient blobClient)
        {
            _blobClient = blobClient;
        }

        [HttpGet]
        [Route("upload")]
        public IActionResult Upload()
        {
            var headItem = (HeadItem) HttpContext.Items[HeadItem.HeadItemKey];
            headItem.PageTitle = "Upload · File";
            return View();
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName).TrimStart('.');
            if (FileDescripter.AllowedExtension.All(e => !e.EqualIgnoreCase(ext)))
                return Ok($"The file with extension [{ext}] is not allowed.");

            if (file.Length > FileDescripter.MaxSizeInKb * 1024)
                return Ok($"The file size [{file.Length / 1024}KB] is too big.");

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                var path = await _blobClient.UploadFileAsync(ms.ToArray(), file.FileName);
                return Ok(path);
            }
        }
    }
}