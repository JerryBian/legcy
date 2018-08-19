using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Laobian.Share.Domain.Blog.RandomPost
{
    /// <summary>
    /// Represents the factor of random post generator rquest
    /// </summary>
    public class RandomPostFactor
    {
        /// <summary>
        /// HTTP request item key
        /// </summary>
        public const string ItemKey = "__randomPost";

        /// <summary>
        /// Gets or sets a collection of post id which represents the result should exclude
        /// </summary>
        [JsonProperty("e_posts")]
        public IEnumerable<Guid> ExcludePosts { get; set; }

        /// <summary>
        /// Gets or sets the post id which represents similar in result
        /// </summary>
        [JsonProperty("a_post")]
        public Guid AlikePost { get; set; }

        /// <summary>
        /// Gets or sets whether the result should be viewed only by Admin
        /// </summary>
        [JsonProperty("admin_view")]
        public bool AdminView { get; set; }
    }
}