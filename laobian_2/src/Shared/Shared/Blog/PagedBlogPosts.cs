using Laobian.Shared.Interface;
using Laobian.Shared.Interface.Blog;
using System.Collections.Generic;

namespace Laobian.Shared.Blog
{
    public class PagedBlogPosts
    {
        public Pagination Pagination { get; set; }

        public IEnumerable<BlogPost> Posts { get; set; }

        public string Url { get; set; }
    }
}
