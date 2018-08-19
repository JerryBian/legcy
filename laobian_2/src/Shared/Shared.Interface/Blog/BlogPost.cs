using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Laobian.Shared.Interface.Blog
{
    public class BlogPost
    {
        private string _htmlDescription;
        private BlogPostContent _contentEn;
        private BlogPostContent _contentZh;

        public BlogPost()
        {
            Contents = new List<BlogPostContent>();
            Tags = new List<BlogTag>();
        }

        public int Id { get; set; }

        public string Url { get; set; }

        public bool AllowComment { get; set; }

        public string HtmlDescription
        {
            get
            {
                if (string.IsNullOrEmpty(_htmlDescription))
                {
                    var doc = new HtmlDocument();
                    doc.LoadHtml(GetByLangWithDefault().Html);
                    _htmlDescription = doc.DocumentNode.FirstChild.OuterHtml;
                }

                return _htmlDescription;
            }
        }

        public DateTime PostDateTime => Contents.Any() ? Contents.Min(_ => _.CreateTime) : default(DateTime);

        public List<BlogPostContent> Contents { get; set; }

        public List<BlogTag> Tags { get; }

        public string TagsString
        {
            get { return string.Join(',', Tags.Select(_ => _.Name)); }
            set
            {
                Tags.Clear();
                Tags.AddRange(value.Split(",").Select(_ => new BlogTag { Name = _ }));
            }
        }

        public BlogPostContent GetByLang(string lang = "en")
        {
            InitContents();
            return BlogLang.IsEnglish(lang) ? _contentEn : _contentZh;
        }

        public BlogPostContent GetByLangWithDefault(string lang = "en")
        {
            return GetByLang(lang) ?? GetByNotLang(lang);
        }

        public BlogPostContent GetByNotLang(string lang)
        {
            InitContents();
            return BlogLang.IsEnglish(lang) ? _contentZh : _contentEn;
        }

        private void InitContents()
        {
            _contentEn = _contentEn ?? Contents.FirstOrDefault(_ => _.IsEnglish);
            _contentZh = _contentZh ?? Contents.FirstOrDefault(_ => !_.IsEnglish);
        }
    }
}
