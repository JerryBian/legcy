using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Newtonsoft.Json;
using ProtoBuf;

namespace Laobian.Share.Model
{
    [ProtoContract(SkipConstructor = true)]
    public class BlogPost : BlobDataBase
    {
        private HtmlDocument _htmlDoc;

        [JsonProperty("title")]
        [ProtoMember(1)]
        public string Title { get; set; }

        [JsonProperty("url")]
        [ProtoMember(2)]
        public string Url { get; set; }

        [JsonProperty("markdown")]
        [ProtoMember(3)]
        public string Markdown { get; set; }

        [JsonProperty("html")]
        [ProtoMember(4)]
        public string Html { get; set; }

        [JsonProperty("is_publish")]
        [ProtoMember(5)]
        public bool IsPublish { get; set; }

        [JsonProperty("is_private")]
        [ProtoMember(6)]
        public bool IsPrivate { get; set; }

        [JsonIgnore] public List<BlogCategory> BlogCategories { get; } = new List<BlogCategory>();

        [JsonIgnore] public int Visit { get; set; }

        [JsonIgnore] public BlogPost PrevPost { get; set; }

        [JsonIgnore] public BlogPost NextPost { get; set; }

        [JsonIgnore] public BlogPost FirstPost { get; set; }

        [JsonIgnore] public BlogPost LastPost { get; set; }

        private List<HtmlNode> GetImgNodes()
        {
            return GetHtmlDoc().DocumentNode.Descendants()
                .Where(_ => _.Name.Equals("img", StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        public string GetFullUrl()
        {
            return $"/{CreateAt.Year:0000}/{CreateAt.Month:00}/{Url}.html";
        }

        public string GetDescriptionText()
        {
            return GetHtmlDoc().DocumentNode.FirstChild.InnerText;
        }

        public string GetThumbnail()
        {
            var imgNodes = GetImgNodes();
            return imgNodes.FirstOrDefault()?.Attributes["src"]?.Value;
        }

        public string GetCategoryLinks()
        {
            var cats = new List<string>();
            foreach (var cat in BlogCategories) cats.Add($"<a href='{cat.GetFullUrl()}'>{cat.Name}</a>");

            return string.Join(", ", cats);
        }

        public string GetCategoryText()
        {
            var cats = new List<string>();
            foreach (var cat in BlogCategories) cats.Add($"{cat.Name}");

            return string.Join(", ", cats);
        }

        private HtmlDocument GetHtmlDoc()
        {
            if (_htmlDoc == null)
            {
                _htmlDoc = new HtmlDocument();
                _htmlDoc.LoadHtml(Html);
            }

            return _htmlDoc;
        }
    }
}