using AccountsManager.DataModels.V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.DataAccess.V1.Contracts
{
    public interface IVoucherRepository : IBaseRepository<Voucher>
    {
        IQueryable<Voucher> GetVouchers(int pageNumber, int pageSize);
    }
}
