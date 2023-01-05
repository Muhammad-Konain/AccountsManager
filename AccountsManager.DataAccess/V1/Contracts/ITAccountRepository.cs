using AccountsManager.DataModels.V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.DataAccess.V1.Contracts
{
    public interface ITAccountRepository : IBaseRepository<TAccount>
    {
        IQueryable<TAccount> GetAccounts(List<Guid> accounts);
    }
}
