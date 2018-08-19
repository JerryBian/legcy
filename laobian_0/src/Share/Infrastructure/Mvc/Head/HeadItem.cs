using System;
using System.Collections.Generic;

namespace Laobian.Share.Infrastructure.Mvc.Head
{
    public class HeadItem
    {
        public const string HeadItemKey = "__headItem";

        public string BaseTitle { get; set; }

        public string PageTitle { get; set; }

        public string ThemeColor { get; set; }

        // Short description of the document (limit to 150 characters)
        public string Description { get; set; }

        public string Robots { get; set; } = "noindex,nofollow";

        public string Canonical { get; set; }

        public string License { get; set; } = "https://creativecommons.org/licenses/by/4.0";

        public List<string> Me { get; set; }

        public string First { get; set; }

        public string Last { get; set; }

        public string Prev { get; set; }

        public string Next { get; set; }

        public string Alternate { get; set; }

        public List<string> DnsPrefetch { get; set; }

        public string Icon { get; set; } = "/favicon.ico";

        // 360 broswer
        public string Rendered { get; set; } = "webkit|ie-comp|ie-stand";

        // UC browser
        public string ImageMode { get; set; } = "force";

        // UC browser
        public string NightMode { get; set; } = "disable";

        // UC browser
        public string LayoutMode { get; set; } = "fitscreen";

        // UC browser
        public string WapFontScale { get; set; } = "no";

        public DateTime? DatePublished { get; set; }

        public DateTime? DateModified { get; set; }

        public string BaseUrl { get; set; }

        public string Author { get; set; }
    }
}