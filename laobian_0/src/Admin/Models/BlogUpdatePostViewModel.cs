using System;
using System.Collections.Generic;
using System.Linq;
using Laobian.Share.Domain.Blog;
using Laobian.Share.Model;
using Newtonsoft.Json;

namespace Laobian.Admin.Models
{
    public class BlogUpdatePostViewModel : BlogPost
    {
        [JsonProperty("categories")] public IEnumerable<Guid> SelectedCategories { get; set; }

        public IEnumerable<BlogCategory> AllCategories { get; set; }

        public BlogPost ToBlogPost()
        {
            var result = JsonConvert.DeserializeObject<BlogPost>(JsonConvert.SerializeObject(this));
            result.BlogCategories.AddRange(SelectedCategories.Select(c => new BlogCategory {Id = c}));
            return result;
        }
    }
}