using IdentityStudy.Repositories;
using IdentityStudy.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityStudy.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMyIdentity(this IServiceCollection services)
        {
            services.AddSingleton<IIdentityRepository, IdentityRepository>();
            services.AddSingleton<IdentityService>();
            return services;
        }
    }
}
