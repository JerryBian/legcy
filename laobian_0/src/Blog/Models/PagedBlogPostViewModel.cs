using System.Collections.Generic;
using Laobian.Share.Domain.Blog;
using Laobian.Share.Model;

namespace Laobian.Blog.Models
{
    public class PagedBlogPostViewModel
    {
        public Pagination Pagination { get; set; }

        public IEnumerable<BlogPost> Posts { get; set; }

        public string Url { get; set; }
    }
}