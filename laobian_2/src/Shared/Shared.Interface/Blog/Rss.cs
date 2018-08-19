using System.Collections.Generic;

namespace Laobian.Shared.Interface.Blog
{
    public class Rss
    {
        public Rss()
        {
            Channel = new Channel();
        }

        public Channel Channel { get; }
    }

    public class Channel
    {
        public Channel()
        {
            Image = new Image();
            Items = new List<Item>();
        }

        public string Title { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public string Language { get; set; }

        public string Copyright { get; set; }

        public string ManagingEditor { get; set; }

        public string WebMaster { get; set; }

        public string PubDate { get; set; }

        public string LastBuildDate { get; set; }

        public string Category { get; set; }

        public string Docs { get; set; }

        public string Ttl { get; set; }

        public Image Image { get; }

        public List<Item> Items { get; }
    }

    public class Image
    {
        public string Url { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }
    }

    public class Item
    {
        public string Title { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }

        public string Comments { get; set; }

        public string Guid { get; set; }

        public string PubDate { get; set; }
    }
}
