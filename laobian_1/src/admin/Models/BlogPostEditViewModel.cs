using System.Collections.Generic;
using Laobian.Share.Model.Blog;

namespace Laobian.Admin.Models
{
    public class BlogPostEditViewModel
    {
        public BlogPostEditViewModel(BlogPost post, IEnumerable<BlogCategory> categories)
        {
            Post = post;
            AllACategories = categories;
        }

        public BlogPost Post { get; set; }

        public IEnumerable<BlogCategory> AllACategories { get; set; }
    }
}
