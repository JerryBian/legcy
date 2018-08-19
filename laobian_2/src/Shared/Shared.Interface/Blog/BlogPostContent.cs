using System;

namespace Laobian.Shared.Interface.Blog
{
    public class BlogPostContent
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public string Markdown { get; set; }

        public string Html { get; set; }

        public string Title { get; set; }

        public int VisitCount { get; set; }

        public bool IsPublic { get; set; }

        public bool IsEnglish { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime LastUpdateTime { get; set; }
    }
}
