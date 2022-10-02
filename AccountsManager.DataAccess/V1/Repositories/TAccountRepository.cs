using AccountsManager.DataAccess.V1.Contracts;
using AccountsManager.DataModels.V1.Data;
using AccountsManager.DataModels.V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.DataAccess.V1.Repositories
{
    public sealed class TAccountRepository : BaseRepository<TAccount>, ITAccountRepository
    {
        public TAccountRepository(AppDBContext context) : base(context)
        {
        }
    }
}
