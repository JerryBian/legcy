using Laobian.Shared.Interface.Blog;
using System.Collections.Generic;

namespace Laobian.Blog.Models
{
    public class ArchivesViewModel
    {
        public string Key { get; set; }

        public string ArchiveLink => $"/archive/{Key}";

        public string SummaryHtml => $"<a href='{ArchiveLink}'>{Key}({Posts.Count})</a>";

        public List<BlogPost> Posts { get; set; }
    }
}
