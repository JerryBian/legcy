using HtmlAgilityPack;
using Markdig;
using Newtonsoft.Json;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Laobian.Common.Blog
{
    /// <summary>
    /// Blog post model
    /// </summary>
    [ProtoContract]
    public class BlogPost
    {
        /// <summary>
        /// Constructor of <see cref="BlogPost"/>
        /// </summary>
        public BlogPost()
        {
            Raw = new PostRawData();
        }

        #region Properties

        /// <summary>
        /// Raw markdown, which will be converted to HTML in runtime
        /// </summary>>
        [JsonProperty("raw")]
        [ProtoMember(1)]
        public PostRawData Raw { get; }

        /// <summary>
        /// Id of post
        /// </summary>
        [JsonIgnore]
        public Guid Id => Guid.Parse(Raw.Id);

        private string _content;

        /// <summary>
        /// HTML content of post
        /// </summary>
        [JsonIgnore]
        public string Content
        {
            get
            {
                if (string.IsNullOrEmpty(_content))
                {
                    _content = AdaptImageTags(Markdown.ToHtml(GetData<string>(Raw.Content)));
                }

                return _content;
            }
        }

        private DateTime _publishTime;

        /// <summary>
        /// Publish time of post
        /// </summary>
        [JsonIgnore]
        public DateTime PublishTime
        {
            get
            {
                if (_publishTime == default)
                {
                    _publishTime = GetData<DateTime>(Raw.PublishTime);
                }

                return _publishTime;
            }
        }

        /// <summary>
        /// Indicates whether post is published,
        /// True if yes, otherwise no
        /// </summary>
        [JsonIgnore]
        public bool Publish => bool.Parse(Raw.Publish);

        private string _declaimer;

        /// <summary>
        /// HTML declaimer of post
        /// </summary>
        [JsonIgnore]
        public string Declaimer
        {
            get
            {
                if (_declaimer == null) // we use null only to detect local private variable
                {
                    _declaimer = Markdown.ToHtml(GetData<string>(Raw.Declaimer) ?? string.Empty);
                }

                return _declaimer;
            }
        }

        private string _excerpt;

        /// <summary>
        /// HTML excerpt of post
        /// </summary>
        [JsonIgnore]
        public string Excerpt
        {
            get
            {
                if (string.IsNullOrEmpty(_excerpt))
                {
                    _excerpt = Markdown.ToHtml(GetData<string>(Raw.Excerpt));
                }

                return _excerpt;
            }
        }

        /// <summary>
        /// Update time of post
        /// </summary>
        [ProtoMember(2)]
        [JsonProperty("updateTime")]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// Category of post
        /// </summary>
        [JsonIgnore]
        public string Category => Raw.Category;

        /// <summary>
        /// URL of post
        /// </summary>
        [JsonIgnore]
        public string Url => Raw.Url;

        /// <summary>
        /// Title of post
        /// </summary>
        [JsonIgnore]
        public string Title => Raw.Title;

        /// <summary>
        /// References of post
        /// </summary>
        [JsonIgnore]
        public List<string> Reference
        {
            get
            {
                var rs = new List<string>();
                if (Raw.Reference == null)
                {
                    return rs;
                }

                rs.AddRange(Raw.Reference.Select(r => Markdown.ToHtml(r)));
                return rs;
            }
        }

        /// <summary>
        /// HTML markup for assets
        /// </summary>
        [JsonIgnore]
        public string AssetHtml
        {
            get
            {
                var html = new List<string>();
                if (Raw.Asset == null)
                {
                    return string.Empty;
                }

                foreach (var asset in Raw.Asset)
                {
                    if (Enum.TryParse(asset, true, out AssetName assetName))
                    {
                        html.Add(AssetLoader.Load(assetName));
                    }
                }

                return string.Join(Environment.NewLine, html);
            }
        }

        /// <summary>
        /// Count of visit of this post,
        /// This is set at runtime, need to call <see cref="SetVisitCount"/> after instance initialized
        /// </summary>
        [JsonIgnore]
        public int VisitCount { get; private set; }

        #endregion

        /// <summary>
        /// Get full URL of post, e.g. /2018/10/hello-world.html
        /// </summary>
        /// <returns>Full URL string</returns>
        public string GetFullUrl()
        {
            return $"/{PublishTime.Year:0000}/{PublishTime.Month:00}/{Url}.html";
        }

        /// <summary>
        /// Set count of visit
        /// </summary>
        /// <param name="count">The count</param>
        public void SetVisitCount(int count)
        {
            VisitCount = count;
        }

        private T GetData<T>(string rawData)
        {
            if (string.IsNullOrEmpty(rawData))
            {
                return default;
            }

            return (T)Convert.ChangeType(rawData, typeof(T));
        }

        private string AdaptImageTags(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var imgNodes = htmlDoc.DocumentNode.Descendants("img").ToList();
            for (var i = imgNodes.Count - 1; i >= 0; i--)
            {
                var imgNode = imgNodes[i];
                var src = imgNode.GetAttributeValue("src", string.Empty);
                var title = imgNode.GetAttributeValue("title", string.Empty);
                var alt = imgNode.GetAttributeValue("alt", string.Empty);

                var caption = string.IsNullOrEmpty(title) ? string.Empty : $"<figcaption >{title}</figcaption>";
                var snippet =
                    $"<figure><img class='img-fluid img-thumbnail mx-auto d-block' src='{src}' alt='{alt}' title='{title}'>{caption}</figure>";

                var figureNode =
                    HtmlNode.CreateNode(snippet);
                imgNode.ParentNode.ReplaceChild(figureNode, imgNode);
            }

            return htmlDoc.DocumentNode.InnerHtml;
        }


    }
}
