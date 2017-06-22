using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Laobian.Infrastuture.Interface.Service;
using Laobian.Infrastuture.Entity.Blog;

namespace Laobian.Admin.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _blogService.GetAllPostsAsync(false);
            return View(model);
        }

        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPut]
        [Route("add")]
        public async Task<IActionResult> Add(BlogPost post)
        {
            await _blogService.AddPostAsync(post);
            return Ok();
        }

        [HttpGet]
        [Route("update/{id:int}")]
        public async Task<IActionResult> Update([FromRoute]int id)
        {
            var model = await _blogService.GetPostAsync(id);
            return View(model);
        }

        [HttpPost]
        [Route("update/{id:int}")]
        public async Task<IActionResult> Update(BlogPost post, [FromRoute]int id)
        {
            post.Id = id;
            await _blogService.UpdatePostAsync(post);
            return Ok();
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            await _blogService.DeletePostAsync(id);
            return Ok();
        }
    }
}
