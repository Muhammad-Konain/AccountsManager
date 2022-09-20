using AccountsManager.DataModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.DataModels.Data
{
    internal class AppContext : DbContext
    {
        public AppContext()
        {
        }
        public AppContext(DbContextOptions<AppContext> options)
           : base(options)
        {
        }

        public DbSet<TAccount> TAccounts { get; set; }
    }
}
