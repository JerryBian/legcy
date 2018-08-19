using System.Threading.Tasks;
using Laobian.Shared.Interface.Blog;
using Microsoft.AspNetCore.Mvc;

namespace Laobian.Blog.Controllers
{
    public class PostController : Controller
    {
        private readonly IBlogService _blogService;

        public PostController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        [Route("/p/{id:int}")]
        public async Task<IActionResult> GetPostById([FromRoute] int id)
        {
            var result = await _blogService.GetBlogPostByIdAsync(id);
            return View("Detail", result);
        }

        [HttpGet]
        [Route("/{year:int}/{month:int}/{url}.html")]
        public async Task<IActionResult> GetPostByUrl([FromRoute] int year, [FromRoute] int month, [FromRoute] string url, [FromQuery] string lang)
        {
            var result = await _blogService.GetBlogPostByUrlAsync(year, month, url);
            if(result == null)
            {
                return NotFound();
            }

            lang = BlogLang.GetNormalizedLang(lang);
            ViewData["lang"] = string.IsNullOrEmpty(lang) ? (result.GetByLang() != null ? BlogLang.English : BlogLang.Chinese) : lang;
            return View("Detail", result);
        }

        [HttpPost]
        [Route("/p/visit/{postContentId:int}")]
        public async Task<IActionResult> IncreasePostVisit([FromRoute] int postContentId)
        {
            await _blogService.IncreasePostVisitAsync(postContentId);
            return Ok();
        }
    }
}