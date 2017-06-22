using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace Laobian.Admin.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "active-index")]
    public class NavbarTagHelper:TagHelper
    {
        [HtmlAttributeName("active-index")]
        public int ActiveIndex { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var content = $@"<nav class='navbar navbar-toggleable-md navbar-inverse fixed-top bg-inverse'>
    <button class='navbar-toggler navbar-toggler-right hidden-lg-up' type='button' data-toggle='collapse' data-target='#navbarsExampleDefault' aria-controls='navbarsExampleDefault' aria-expanded='false' aria-label='Toggle navigation'>
        <span class='navbar-toggler-icon'></span>
    </button>
    <a class='navbar-brand' href='#'>Dashboard</a>

    <div class='collapse navbar-collapse' id='navbarsExampleDefault'>
        <ul class='navbar-nav mr-auto'>"+
        CreateLink("Home", 0, "/") +
        CreateLink("Blog", 1, "/blog") +
        @"</ul>
    </div>
</nav>";
            output.Content.AppendHtml(content);
            base.Process(context, output);
        }

        private string CreateLink(string name, int index, string href)
        {
            var result = new StringBuilder();
            if(ActiveIndex == index)
            {
                result.Append("<li class='nav-item active'>");
                result.Append($"<a class='nav-link' href='{href}'>{name} <span class='sr-only'>(current)</span></a>");
            }
            else
            {
                result.Append("<li class='nav-item'>");
                result.Append($"<a class='nav-link' href='{href}'>{name}</a>");
            }

            result.Append("</li>");
            return result.ToString();
        }
    }
}
