using System.Threading.Tasks;
using Laobian.Share.Domain.Blog.RandomPost;
using Laobian.Share.Domain.Interface;
using Laobian.Share.Infrastructure.Mvc.Head;
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
        [Route("/{year:int}/{month:int}/{url}.html")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> GetPostByUrl([FromRoute] int year, [FromRoute] int month,
            [FromRoute] string url)
        {
            var result = await _blogService.FindPostAsync(year, month, url);
            if (result == null) return NotFound();

            await _blogService.AddPostVisitAsync(result.Id);

            var headItem = (HeadItem)HttpContext.Items[HeadItem.HeadItemKey];
            headItem.Canonical = $"{result.GetFullUrl()}";
            headItem.PageTitle = result.Title;
            headItem.Description = $"{result.GetDescriptionText()}";
            headItem.DatePublished = result.CreateAt;
            headItem.DateModified = result.LastUpdateAt;

            headItem.First = result.FirstPost?.GetFullUrl();

            headItem.Last = result.LastPost?.GetFullUrl();

            headItem.Prev = result.PrevPost?.GetFullUrl();

            headItem.Next = result.NextPost?.GetFullUrl();

            var randomPostItem = (RandomPostFactor)HttpContext.Items[RandomPostFactor.ItemKey];
            randomPostItem.AlikePost = result.Id;

            return View("Detail", result);
        }

        [HttpPost]
        [Route("/{year:int}/{month:int}/{url}.html")]
        public async Task<IActionResult> AddPostVisitAsync([FromRoute] int year, [FromRoute] int month,
            [FromRoute] string url)
        {
            await _blogService.AddPostVisitAsync(year, month, url);
            return Ok();
        }
    }
}