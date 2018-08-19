using System;
using System.Collections.Generic;
using Laobian.Share.Model;

namespace Laobian.Blog.Models
{
    public class BlogCategoryViewModel
    {
        public BlogCategoryViewModel(List<BlogPost> posts, List<BlogCategory> categories)
        {
            Posts = posts;
            Categories = categories;
        }

        public List<BlogPost> Posts { get; set; }

        public List<BlogCategory> Categories { get; set; }

        public Guid SelecteCategory { get; set; }
    }
}