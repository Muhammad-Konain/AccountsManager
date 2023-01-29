using AccountsManager.DataAccess.V1.Contracts;
using AccountsManager.DataModels.V1.Data;
using AccountsManager.DataModels.V1.Models;

namespace AccountsManager.DataAccess.V1.Repositories
{
    public sealed class TAccountRepository : BaseRepository<TAccount>, ITAccountRepository
    {
        public TAccountRepository(AppDBContext context) : base(context)
        {
        }
        public IQueryable<TAccount> GetAccounts(List<Guid> accounts)
        {
            return Find(a => accounts.Contains(a.Id));
        }
        public IQueryable<TAccount> GetAccounts(int pageNumber, int pageSize)
        {
            var pageState = GetAll().OrderBy(o => o.AccountType);

            if (pageNumber > 1)
                return pageState.Skip(pageSize * pageNumber).Take(pageSize);

            return pageState.Take(pageSize);
        }
    }
}
