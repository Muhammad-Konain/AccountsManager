﻿using AccountsManager.DataAccess.V1.Contracts;
using AccountsManager.DataModels.V1.Data;
using AccountsManager.DataModels.V1.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountsManager.DataAccess.V1.Repositories
{
    public sealed class VoucherRepository : BaseRepository<Voucher>, IVoucherRepository
    {
        public VoucherRepository(AppDBContext context): base(context)
        {
        }

        public override IQueryable<Voucher> GetAll()
        {
            return base.GetAll()
                       .Include(i => i.Transactions);
        }
        public override IQueryable<Voucher> GetById(Guid entityID)
        {
            return base.GetById(entityID)
                       .Include(i => i.Transactions);
        }
    }
}
