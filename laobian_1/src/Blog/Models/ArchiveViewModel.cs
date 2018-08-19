using System.Collections.Generic;
using Laobian.Share.Model.Blog;

namespace Laobian.Blog.Models
{
    public class ArchiveViewModel
    {
        public ArchiveViewModel(List<BlogPost> posts, List<DateViewModel> dates)
        {
            Posts = posts;
            Dates = dates;
        }

        public List<BlogPost> Posts { get; set; }

        public string SelectedDate { get; set; }

        public List<DateViewModel> Dates { get; set; }
    }
}
