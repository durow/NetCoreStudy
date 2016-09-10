using Microsoft.AspNetCore.Builder;
using MiddlewareStudy.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareStudy.Extensions
{
    public static class AppExtensions
    {
        public static IApplicationBuilder UseTimeMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<TimeMiddleware>();
            return app;
        }
    }
}
