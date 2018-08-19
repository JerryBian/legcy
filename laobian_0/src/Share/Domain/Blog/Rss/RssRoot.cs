using System.Xml.Serialization;

namespace Laobian.Share.Domain.Blog.Rss
{
    [XmlRoot("rss")]
    public class RssRoot
    {
        [XmlAttribute("version")] public string Version { get; set; }

        [XmlElement("channel")] public RssChannel Channel { get; set; }
    }
}