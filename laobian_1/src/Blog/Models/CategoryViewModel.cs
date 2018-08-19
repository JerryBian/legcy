using System;
using System.Collections.Generic;
using Laobian.Share.Model.Blog;

namespace Laobian.Blog.Models
{
    public class CategoryViewModel
    {
        public CategoryViewModel(List<BlogPost> posts, IEnumerable<BlogCategory> categories)
        {
            Posts = posts;
            Categories = categories;
        }

        public List<BlogPost> Posts { get; set; }

        public IEnumerable<BlogCategory> Categories { get; set; }

        public int SelecteCategory { get; set; }
    }
}
