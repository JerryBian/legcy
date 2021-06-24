using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Laobian.Common.Blog
{
    /// <summary>
    /// Blog assets loader
    /// </summary>
    public static class AssetLoader
    {
        private static readonly ConcurrentDictionary<AssetName, string> CachedAssets = new ConcurrentDictionary<AssetName, string>();

        /// <summary>
        /// Load asset
        /// </summary>
        /// <param name="asset">The specified asset</param>
        /// <returns>HTML markup</returns>
        /// <exception cref="NotSupportedException">If asset name is not supported, exception thrown</exception>
        public static string Load(AssetName asset)
        {
            switch (asset)
            {
                case AssetName.Highlight:
                    return CachedAssets.GetOrAdd(AssetName.Highlight, name =>
                    {
                        var snippets = new List<string>
                        {
                            BuildStyle(
                                "https://cdnjs.cloudflare.com/ajax/libs/highlight.js/9.15.6/styles/googlecode.min.css"),
                            BuildScript("https://cdnjs.cloudflare.com/ajax/libs/highlight.js/9.15.6/highlight.min.js",
                                false),
                            BuildOnDomLoadedScript("hljs.initHighlightingOnLoad()")
                        };
                        return string.Join(Environment.NewLine, snippets);
                    });
                case AssetName.Jquery:
                    return CachedAssets.GetOrAdd(AssetName.Jquery, name =>
                    {
                        var snippets = new List<string>
                        {
                            BuildScript("http://code.jquery.com/jquery-2.2.4.min.js", false)
                        };
                        return string.Join(Environment.NewLine, snippets);
                    });
                case AssetName.MathJax:
                    return CachedAssets.GetOrAdd(AssetName.MathJax, name =>
                    {
                        var snippets = new List<string>
                        {
                            BuildScript(
                                "https://cdnjs.cloudflare.com/ajax/libs/mathjax/2.7.5/MathJax.js?config=TeX-MML-AM_CHTML")
                        };
                        return string.Join(Environment.NewLine, snippets);
                    });
                default:
                    throw new NotSupportedException();
            }
        }

        private static string BuildStyle(string url)
        {
            return $"<link rel=\"stylesheet\" href=\"{url}\" />";
        }

        private static string BuildScript(string url, bool async = true)
        {
            return async ?
                $"<script async src=\"{url}\"></script>" :
                $"<script src=\"{url}\"></script>";
        }

        private static string BuildOnDomLoadedScript(string script)
        {
            return $@"<script>document.addEventListener('DOMContentLoaded', function () {{{script}}}, false)</script>";
        }
    }
}
