using System.Collections.Generic;

namespace Laobian.Shared.Interface.Blog
{
    public class Sitemap
    {
        public Sitemap()
        {
            Urlset = new Urlset();
        }

        public Urlset Urlset { get; set; }
    }

    public class Urlset
    {
        public Urlset()
        {
            Urls = new List<Url>();
        }

        public List<Url> Urls { get; }
    }

    public class Url
    {
        public string Loc { get; set; }

        public string Lastmod { get; set; }

        public string Changefreq { get; set; }

        public string Priority { get; set; }
    }
}
