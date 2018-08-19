using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Laobian.Share.Domain.Blog.Rss
{
    public class ChannelItem
    {
        [XmlElement("title")] public string Title { get; set; }

        [XmlElement("link")] public string Link { get; set; }

        [XmlElement("description")] public string Description { get; set; }

        [XmlIgnore] public DateTime PubDate { get; set; }

        [XmlElement("pubDate")]
        public string PubDateString
        {
            get => PubDate.ToString("r");
            set => PubDate = DateTime.Parse(value);
        }

        [XmlElement("guid")] public string Guid { get; set; }

        [XmlElement("author")] public string Author { get; set; }

        [XmlElement("category")] public List<string> Category { get; set; }
    }
}