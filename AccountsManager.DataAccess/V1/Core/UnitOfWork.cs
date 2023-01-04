using AccountsManager.DataAccess.V1.Contracts;
using AccountsManager.DataModels.V1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.DataAccess.V1.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext _context;
        public ITAccountRepository AccountRepository { get; init; }
        public IVoucherRepository VoucherRepository { get; init; }
        public ITransactionRepository TransactionRepository { get; init; }

        public UnitOfWork(AppDBContext context, ITAccountRepository accountRepository, IVoucherRepository voucherRepository, ITransactionRepository transactionRepository)
        {
            _context = context;
            AccountRepository = accountRepository;
            VoucherRepository = voucherRepository;
            TransactionRepository = transactionRepository;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
