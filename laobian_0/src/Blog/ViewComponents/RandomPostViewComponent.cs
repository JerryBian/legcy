using System.Threading.Tasks;
using Laobian.Share.Domain.Blog.RandomPost;
using Laobian.Share.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Laobian.Blog.ViewComponents
{
    public class RandomPostViewComponent : ViewComponent
    {
        private readonly IRandomPostGenerator _randomPostGenerator;

        public RandomPostViewComponent(IRandomPostGenerator randomPostGenerator)
        {
            _randomPostGenerator = randomPostGenerator;
        }

        public async Task<IViewComponentResult> InvokeAsync(RandomPostFactor factor)
        {
            var model = await _randomPostGenerator.Execute(factor);
            return View(model);
        }
    }
}