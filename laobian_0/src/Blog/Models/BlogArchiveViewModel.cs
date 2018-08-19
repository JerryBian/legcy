using System.Collections.Generic;
using Laobian.Share.Model;

namespace Laobian.Blog.Models
{
    public class BlogArchiveViewModel
    {
        public BlogArchiveViewModel(List<BlogPost> posts, List<DateTimeViewModel> dates)
        {
            Posts = posts;
            Dates = dates;
        }

        public List<BlogPost> Posts { get; set; }

        public string SelectedDate { get; set; }

        public List<DateTimeViewModel> Dates { get; set; }
    }
}