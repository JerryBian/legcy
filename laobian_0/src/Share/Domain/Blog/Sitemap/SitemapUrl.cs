using System;
using System.Xml.Serialization;

namespace Laobian.Share.Domain.Blog.Sitemap
{
    public class SitemapUrl
    {
        [XmlElement("loc")] public string Loc { get; set; }

        [XmlElement("lastmod", DataType = "date")]
        public DateTime LastMod { get; set; }

        [XmlElement("changefreq")] public string ChangeFreq { get; set; }

        [XmlElement("priority")] public string Priority { get; set; }
    }
}