using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laobian.Infrastuture.Middleware
{
    public static class ForwardMiddleware
    {
        public static void Use(IApplicationBuilder app)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All,
                ForwardedHostHeaderName = "Host",
                OriginalForHeaderName = "X-Forwarded-For",
                OriginalHostHeaderName = "Host",
                OriginalProtoHeaderName = "X-Forwarded-Proto"
            });
        }
    }
}
