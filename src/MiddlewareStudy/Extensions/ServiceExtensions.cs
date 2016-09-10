using Microsoft.Extensions.DependencyInjection;
using MiddlewareStudy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareStudy.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddTimeMiddleware(this IServiceCollection services)
        {
            services.AddSingleton<StopWatch>();
            return services;
        }
    }
}
