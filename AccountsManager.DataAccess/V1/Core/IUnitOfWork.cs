using AccountsManager.DataAccess.V1.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.DataAccess.V1.Core
{
    public interface IUnitOfWork
    {
        ITAccountRepository AccountRepository { get; }
        IVoucherRepository VoucherRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
