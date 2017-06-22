using Laobian.Admin.Const;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System;

namespace Laobian.Admin.TagHelpers
{
    [HtmlTargetElement("aside",Attributes = "category, active-index")]
    public class SidebarTagHelper : TagHelper
    {
        [HtmlAttributeName("active-index")]
        public int ActiveIndex { get; set; }
        
        [HtmlAttributeName("category")]
        public SidebarCategory Category { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var content = @"<nav class='col-sm-3 col-md-2 hidden-xs-down bg-faded sidebar'>
            <ul class='nav nav-pills flex-column'>"+
               @""+
               CreateSidebar()+
            @"</ul><ul class='nav nav-pills flex-column'>
<li class='nav-item'><a class='nav-link' href='javascript:;' onclick='showUploadFileModal()'><i class='fa fa-file'></i> Upload File</a></li>
</ul></nav>";
            output.Content.AppendHtml(content);
            base.Process(context, output);
        }

        private string CreateSidebar()
        {
            switch (Category)
            {
                case SidebarCategory.None:
                    return string.Empty;
                case SidebarCategory.Blog:
                    return CreateBlogSidebar();
                default:
                    throw new NotImplementedException();
            }
        }

        private string CreateBlogSidebar()
        {
            var links = new Dictionary<string, string>();
            links.Add("<i class='fa fa-list'></i> Overview", "/blog");
            links.Add("<i class='fa fa-plus'></i> Add new", "/blog/add");

            var result = new StringBuilder();
            for(var i = 0; i < links.Count; i++)
            {
                var current = links.ElementAt(i);
                result.Append("<li class='nav-item'>");
                if (ActiveIndex == i)
                {
                    result.Append($"<a class='nav-link active' href='{current.Value}'>{current.Key} <span class='sr-only'>(current)</span></a>");
                }
                else
                {
                    result.Append($"<a class='nav-link' href='{current.Value}'>{current.Key}</a>");
                }
                result.Append("</li>");
            }

            return result.ToString();
        }
    }
}
