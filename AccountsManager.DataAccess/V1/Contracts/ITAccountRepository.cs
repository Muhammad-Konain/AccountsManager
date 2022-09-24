using AccountsManager.DataModels.V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.DataAccess.V1.Contracts
{
    internal interface ITAccountRepository : IBaseRepository<TAccount>
    {
        //TAccount Create(TAccount tAccount);
        //TAccount Update(TAccount tAccount);
        //IQueryable<TAccount> GetAll();
        //IQueryable<TAccount> GetById(Guid accountId);
    }
}
