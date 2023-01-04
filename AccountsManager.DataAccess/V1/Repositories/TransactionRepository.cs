using AccountsManager.DataAccess.V1.Contracts;
using AccountsManager.DataModels.V1.Data;
using AccountsManager.DataModels.V1.Models;

namespace AccountsManager.DataAccess.V1.Repositories
{
    public sealed class TransactionRepository: BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(AppDBContext context) : base(context)
        {
        }
    }
}
