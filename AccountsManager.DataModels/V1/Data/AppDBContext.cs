using AccountsManager.DataModels.V1.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountsManager.DataModels.V1.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
           : base(options)
        {
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveMaxLength(300);
            base.ConfigureConventions(configurationBuilder);
        }
        public DbSet<TAccount> TAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
    }
}
