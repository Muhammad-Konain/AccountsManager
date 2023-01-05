using AccountsManager.DataAccess.V1.Contracts;
using AccountsManager.DataAccess.V1.Core;
using AccountsManager.DataAccess.V1.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.DataAccess.V1.Registery
{
    public static class DataDependency
    {
        public static IServiceCollection RegisterDataRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITAccountRepository, TAccountRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            return services;
        }
    }
}
