using System.Collections.Generic;
using System.Xml.Serialization;

namespace Laobian.Share.Domain.Blog.Sitemap
{
    [XmlRoot("urlset")]
    public class SitemapUrlSet
    {
        [XmlElement("url")] public List<SitemapUrl> Urls { get; set; } = new List<SitemapUrl>();
    }
}