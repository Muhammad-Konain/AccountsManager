using AccountsManager.Application.V1.Contracts;
using AccountsManager.Application.V1.Helpers;
using AccountsManager.Application.V1.Profiles;
using AccountsManager.Application.V1.Services;
using AccountsManager.ApplicationModels.V1.Validators.TAccountValidators;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountsManager.Application.V1.Registery
{
    public static class ServiceDependency
    {
        public static IServiceCollection RegisterBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<ITAccountService, TAccountService>();
            services.AddScoped<IVoucherService, VoucherService>();
            return services;
        }
        public static IServiceCollection RegisterHelpers(this IServiceCollection services)
        {
            services.AddAutoMapper(config => {
                config.AddProfile(new AutoMapperProfiles());
            });
            services.AddScoped<MappingHelper>();
            services.AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<TAccountCreateDTOValidator>());
               
            return services;
        }
    }
}
