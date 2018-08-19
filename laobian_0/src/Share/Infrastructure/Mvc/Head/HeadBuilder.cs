using Laobian.Share.Utility.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Laobian.Share.Infrastructure.Mvc.Head
{
    public class HeadBuilder
    {
        public static string Execute(HeadItem headItem)
        {
            var title = string.IsNullOrEmpty(headItem.PageTitle)
                ? headItem.BaseTitle
                : string.Format("{0} - {1}", headItem.PageTitle, headItem.BaseTitle);
            var sb = new StringBuilder(64);
            CreateHead(sb, "<meta charset=\"utf-8\">");
            CreateHead(sb,
                "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1, shrink-to-fit=no\">");
            CreateHead(sb, "<title>{0}</title>", title);
            CreateHead(sb, "<meta name=\"theme-color\" content=\"{0}\">", headItem.ThemeColor);
            CreateHead(sb, "<meta name=\"description\" content=\"{0}\">",
                new string(headItem.Description?.Take(149).ToArray()));
            CreateHead(sb, "<meta name=\"robots\" content=\"{0}\">", headItem.Robots);
            CreateHead(sb, "<meta name=\"format-detection\" content=\"telephone=no\">");
            CreateHead(sb, "<link rel=\"canonical\" href=\"{0}\">", headItem.Canonical);
            CreateHead(sb, "<link rel=\"license\" href=\"{0}\">", headItem.License);
            CreateHead(sb, "<link rel=\"me\" href=\"{0}\">", headItem.Me);
            CreateHead(sb, "<link rel=\"first\" href=\"{0}\">", headItem.First);
            CreateHead(sb, "<link rel=\"last\" href=\"{0}\">", headItem.Last);
            CreateHead(sb, "<link rel=\"prev\" href=\"{0}\">", headItem.Prev);
            CreateHead(sb, "<link rel=\"next\" href=\"{0}\">", headItem.Next);
            CreateHead(sb,
                string.Format("<link rel=\"alternate\" href=\"{0}\" type=\"application/rss+xml\" title=\"{1}\">", "{0}",
                    headItem.BaseTitle),
                headItem.Alternate);
            CreateHead(sb, "<link rel=\"dns-prefetch\" href=\"{0}\">", headItem.DnsPrefetch);
            CreateHead(sb, "<link rel=\"icon\" sizes=\"128x128\" href=\"{0}\">", headItem.Icon);
            CreateHead(sb, "<link rel=\"renderer\" content=\"{0}\">", headItem.Rendered);
            CreateHead(sb, "<link rel=\"imagemode\" content=\"{0}\">", headItem.ImageMode);
            CreateHead(sb, "<link rel=\"nightmode\" content=\"{0}\">", headItem.NightMode);
            CreateHead(sb, "<link rel=\"layoutmode\" content=\"{0}\">", headItem.LayoutMode);
            CreateHead(sb, "<link rel=\"wap-font-scale\" content=\"{0}\">", headItem.WapFontScale);
            CreateGoogleStructuredDate(sb, headItem);

            return sb.ToString();
        }

        private static void CreateHead(StringBuilder sb, string head)
        {
            if (!string.IsNullOrEmpty(head)) sb.AppendLine(head);
        }

        private static void CreateHead(StringBuilder sb, string head, string value)
        {
            if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(head))
            {
                sb.AppendFormat(head, value);
                sb.Append(Environment.NewLine);
            }
        }

        private static void CreateHead(StringBuilder sb, string head, IEnumerable<string> values)
        {
            if (!string.IsNullOrEmpty(head) && values != null)
                foreach (var value in values)
                    if (!string.IsNullOrEmpty(value))
                    {
                        sb.AppendFormat(head, value);
                        sb.Append(Environment.NewLine);
                    }
        }

        private static void CreateGoogleStructuredDate(
            StringBuilder sb, 
            HeadItem headItem)
        {
            if (!string.IsNullOrEmpty(headItem.Canonical) &&
                !string.IsNullOrEmpty(headItem.Author) &&
                !string.IsNullOrEmpty(headItem.Icon) &&
                !string.IsNullOrEmpty(headItem.BaseTitle) &&
                !string.IsNullOrEmpty(headItem.Description) &&
                !string.IsNullOrEmpty(headItem.BaseUrl) &&
                headItem.DateModified != null &&
                headItem.DatePublished != null)
            {
                var sd = new StructuredData
                {
                    Author = new Author { Name = headItem.Author},
                    DatePublished = headItem.DatePublished.Value.ToString("O"),
                    DateModified = headItem.DateModified.Value.ToString("O"),
                    Description = headItem.Description,
                    Headline = string.IsNullOrEmpty(headItem.PageTitle)
                        ? headItem.BaseTitle
                        : string.Format("{0} - {1}", headItem.PageTitle, headItem.BaseTitle),
                    Image = new List<string> { $"{headItem.BaseUrl}{headItem.Icon}" },
                    Publisher = new Publisher
                    {
                        Logo = new Logo { Url = $"{headItem.BaseUrl}{headItem.Icon}"}
                    },
                    MainEntityOfPage = new MainEntityOfPage
                    {
                        Id = $"{headItem.BaseUrl}{headItem.Canonical}"
                    }
                };

                sb.AppendLine(
                    $"<script type=\"application/ld+json\">{SerializeHelper.SerializeToJson(sd, Formatting.Indented)}</script>");
            }
        }
    }
}