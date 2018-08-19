using Laobian.Shared.Interface;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Laobian.Blog.TagHelpers
{
    public class PaginationTagHelper : TagHelper
    {
        private const string PaginationParameterName = "p";

        public Pagination Pagination { get; set; }

        public string Url { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "nav";
            output.Attributes.SetAttribute("id", "pagination");

            var ul = "<ul class='pagination justify-content-center'>";
            if (Pagination.CurrentPage > 1)
            {
                ul += $@"<li class='page-item'>
        <a class='page-link' href='{Url}?{PaginationParameterName}={Pagination.CurrentPage - 1}' aria-label='Previous'>
        <span aria-hidden='true'>prev</span>
        <span class='sr-only'>Previous</span>
      </a>
    </li>";
            }

            if (Pagination.TotalPages <= 6)
            {
                for (var i = 1; i <= Pagination.TotalPages; i++)
                {
                    if (Pagination.CurrentPage == i)
                    {
                        ul += $"<li class='page-item active'><span>{i}</span></li>";
                    }
                    else
                    {
                        ul += $"<li class='page-item'><a class='page-link' href='{Url}?{PaginationParameterName}={i}'>{i}</a></li>";
                    }
                }
            }
            else
            {
                if(Pagination.CurrentPage < 5)
                {
                    for (var i = 1; i <= 5; i++)
                    {
                        if (Pagination.CurrentPage == i)
                        {
                            ul += $"<li class='page-item active'><span>{i}</span></li>";
                        }
                        else
                        {
                            ul += $"<li class='page-item'><a class='page-link' href='{Url}?{PaginationParameterName}={i}'>{i}</a></li>";
                        }
                    }

                    ul += "<li class=\'page-item\'><span>...</span></li>";
                    ul += $"<li class='page-item'><a class='page-link' href='{Url}?{PaginationParameterName}={Pagination.TotalPages}'>{Pagination.TotalPages}</a></li>";
                }
                else
                {
                    ul += $"<li class='page-item'><a class='page-link' href='{Url}?{PaginationParameterName}={1}'>{1}</a></li>";
                    ul += "<li class=\'page-item\'><span>...</span></li>";
                    for (var i = Pagination.CurrentPage - 2; i <= Pagination.CurrentPage + 2; i++)
                    {
                        if (Pagination.CurrentPage == i)
                        {
                            ul += $"<li class='page-item active'><span>{i}</span></li>";
                        }
                        else
                        {
                            ul += $"<li class='page-item'><a class='page-link' href='{Url}?{PaginationParameterName}={i}'>{i}</a></li>";
                        }
                    }

                    ul += "<li class=\'page-item\'><span>...</span></li>";
                    ul += $"<li class='page-item'><a class='page-link' href='{Url}?{PaginationParameterName}={Pagination.TotalPages}'>{Pagination.TotalPages}</a></li>";
                }
            }

            if(Pagination.CurrentPage != Pagination.TotalPages)
            {
                ul += $@"<li class='page-item'>
        <a class='page-link' href='{Url}?{PaginationParameterName}={Pagination.CurrentPage + 1}' aria-label='Next'>
        <span aria-hidden='true'>next</span>
        <span class='sr-only'>Next</span>
      </a>
    </li>";
            }

            ul += "</ul>";
            output.Content.SetHtmlContent(ul);
        }
    }
}
