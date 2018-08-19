using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Laobian.Admin.Models;
using Laobian.Share.Data.AzureBlob;
using Laobian.Share.Model.Blob;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Laobian.Admin.Controllers
{
    [Route("file")]
    public class FileController : Controller
    {
        private readonly IAzureBlobClient _blobClient;

        public FileController(IAzureBlobClient blobClient)
        {
            _blobClient = blobClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromForm]IFormFile file)
        {
            var baseType = Request.Headers["Base-Type"];
            if (string.IsNullOrEmpty(baseType))
            {
                return BadRequest("Base Type is empty");
            }

            var fileTypes = SharedFileTypeFactory.AllFileTypes.Where(t => t.BaseType == baseType);
            if (!fileTypes.Any())
            {
                return BadRequest($"Invalid base type {baseType}");
            }

            if (fileTypes.FirstOrDefault(t => t.Type == Path.GetExtension(file.FileName).Substring(1)) == null)
            {
                return BadRequest($"File type {Path.GetExtension(file.FileName)} is not allowed for {baseType}");
            }

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                var path = await _blobClient.UploadBlobAsync(AzureBlobFactory.PublicContainer, Request.Headers["Base-Type"], Path.GetFileName(file.FileName),
                    ms.ToArray());
                return Ok(path);
            }
        }

        [HttpPost]
        [Route("list")]
        public async Task<IActionResult> List()
        {
            var results = await _blobClient.ListBlobsAsync(AzureBlobFactory.PublicContainer);
            foreach (var blobAttribute in results)
            {
                switch (blobAttribute.BaseType)
                {
                    case SharedFileTypeFactory.Image:
                        blobAttribute.ImageHtml = $"<img src='{blobAttribute.Uri}' class='img-thumbnail img-fluid'/>";
                        break;
                    case SharedFileTypeFactory.Doc:
                        blobAttribute.ImageHtml = $"<i class='far fa-file-word fa-10x'></i>";
                        break;
                    case SharedFileTypeFactory.Media:
                        blobAttribute.ImageHtml = $"<i class='far fa-file-audio fa-10x'></i>";
                        break;
                    case SharedFileTypeFactory.Package:
                        blobAttribute.ImageHtml = $"<i class='far fa-file-archive fa-10x'></i>";
                        break;
                    case SharedFileTypeFactory.Text:
                        blobAttribute.ImageHtml = $"<i class='far fa-file fa-10x'></i>";
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }

            return Ok(results);
        }
    }
}