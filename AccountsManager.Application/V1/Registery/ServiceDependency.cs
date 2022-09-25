using AccountsManager.Application.V1.Helpers;
using AccountsManager.Application.V1.Profiles;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.Application.V1.Registery
{
    public static class ServiceDependency
    {
        public static IServiceCollection RegisterBusinessServices(this IServiceCollection services)
        {
            return services;
        }
        public static IServiceCollection RegisterHelpers(this IServiceCollection services)
        {
            services.AddAutoMapper(config => {
                config.AddProfile(new AutoMapperProfiles());
            });
            services.AddScoped<MappingHelper>();
            return services;
        }
    }
}
