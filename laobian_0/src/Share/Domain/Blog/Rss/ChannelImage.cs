using System.Xml.Serialization;

namespace Laobian.Share.Domain.Blog.Rss
{
    public class ChannelImage
    {
        [XmlElement("url")] public string Url { get; set; }

        [XmlElement("title")] public string Title { get; set; }

        [XmlElement("link")] public string Link { get; set; }
    }
}