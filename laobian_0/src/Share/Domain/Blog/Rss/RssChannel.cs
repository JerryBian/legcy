using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Laobian.Share.Domain.Blog.Rss
{
    public class RssChannel
    {
        [XmlElement("title")] public string Title { get; set; }

        [XmlElement("link")] public string Link { get; set; }

        [XmlElement("copyright")] public string Copyright { get; set; }

        [XmlElement("description")] public string Description { get; set; }

        [XmlElement("language")] public string Language { get; set; }

        [XmlIgnore] public DateTime PubDate { get; set; }

        [XmlElement("pubDate")]
        public string PubDateString
        {
            get => PubDate.ToString("r");
            set => PubDate = DateTime.Parse(value);
        }

        [XmlIgnore] public DateTime LastBuildDate { get; set; }

        [XmlElement("lastBuildDate")]
        public string LastBuildDateString
        {
            get => LastBuildDate.ToString("r");
            set => LastBuildDate = DateTime.Parse(value);
        }

        [XmlElement("docs")] public string Docs { get; set; }

        [XmlElement("generator")] public string Generator { get; set; }

        [XmlElement("managingEditor")] public string ManagingEditor { get; set; }

        [XmlElement("webMaster")] public string WebMaster { get; set; }

        [XmlElement("category")] public List<string> Category { get; set; }

        [XmlElement("ttl")] public int Ttl { get; set; }

        [XmlElement("image")] public ChannelImage Image { get; set; }

        [XmlElement("item")] public List<ChannelItem> Items { get; set; } = new List<ChannelItem>();
    }
}