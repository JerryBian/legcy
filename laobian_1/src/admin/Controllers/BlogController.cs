using System.Threading.Tasks;
using Laobian.Admin.Models;
using Laobian.Share.Data.Repository;
using Laobian.Share.Model.Blog;
using Microsoft.AspNetCore.Mvc;

namespace Laobian.Admin.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Category Management

        [HttpGet]
        [Route("category")]
        public async Task<IActionResult> Category()
        {
            var model = await _blogRepository.FindAllCategooriesAsync();
            return View(model);
        }

        [HttpGet]
        [Route("category/new")]
        public IActionResult NewCategory()
        {
            return View();
        }

        [HttpPut]
        [Route("category/new")]
        public async Task<IActionResult> NewCategory([FromBody]BlogCategory category)
        {
            await _blogRepository.AddCategoryAsync(category);
            return Ok();
        }

        [HttpGet]
        [Route("category/{id}")]
        public async Task<IActionResult> EditCategory([FromRoute] int id)
        {
            var model = await _blogRepository.FindCategoryAsync(id);
            return View(model);
        }

        [HttpPost]
        [Route("category/{id}")]
        public async Task<IActionResult> EditCategory([FromBody] BlogCategory category, [FromRoute] int id)
        {
            category.Id = id;
            await _blogRepository.UpdateCategoryAsync(category);
            return Ok();
        }

        [HttpDelete]
        [Route("category/{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute]int id)
        {
            await _blogRepository.DeleteCategoryAsync(id);
            return Ok();
        }

        #endregion

        #region Post Management

        [HttpGet]
        [Route("post")]
        public async Task<IActionResult> Post()
        {
            var model = await _blogRepository.FindAllPostsAsync();
            return View(model);
        }

        [HttpGet]
        [Route("post/new")]
        public async Task<IActionResult> NewPost()
        {
            var model = await _blogRepository.FindAllCategooriesAsync();
            return View(model);
        }

        [HttpPut]
        [Route("post/new")]
        public async Task<IActionResult> NewPost([FromBody] BlogPost post)
        {
            await _blogRepository.AddPostAsync(post);
            return Ok();
        }

        [HttpGet]
        [Route("post/{id}")]
        public async Task<IActionResult> EditPost([FromRoute] int id)
        {
            var post = await _blogRepository.FindPostAsync(id);
            var categories = await _blogRepository.FindAllCategooriesAsync();
            var model = new BlogPostEditViewModel(post, categories);
            return View(model);
        }

        [HttpPost]
        [Route("post/{id}")]
        public async Task<IActionResult> EditPost([FromBody] BlogPost post)
        {
            await _blogRepository.UpdatePostAsync(post);
            return Ok();
        }

        [HttpPost]
        [Route("post/publish/{id}")]
        public async Task<IActionResult> TogglePostPublish([FromRoute] int id)
        {
            await _blogRepository.TogglePostPublishAsync(id);
            return Ok();
        }

        #endregion

    }
}