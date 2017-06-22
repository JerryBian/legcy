using Microsoft.AspNetCore.Mvc;

namespace Laobian.Blog.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
