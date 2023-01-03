using AccountsManager.DataAccess.V1.Contracts;
using AccountsManager.DataModels.V1.Data;
using AccountsManager.DataModels.V1.Models;

namespace AccountsManager.DataAccess.V1.Repositories
{
    public sealed class VoucherRepository : BaseRepository<Voucher>, IVoucherRepository
    {
        public VoucherRepository(AppDBContext context): base(context)
        {
        }
    }
}
