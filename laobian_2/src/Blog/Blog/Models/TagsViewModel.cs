using System.Collections;
using System.Collections.Generic;
using Laobian.Shared.Interface.Blog;

namespace Laobian.Blog.Models
{
    public class TagsViewModel
    {
        public string Name { get; set; }

        public int Count { get; set; }

        public string TagLink => $"/tag/{Name}";

        public string SummaryHtml => $"<a href='{TagLink}'>{Name}({Count})</a>";

        public IEnumerable<BlogPost> Posts { get; set; }
    }
}
