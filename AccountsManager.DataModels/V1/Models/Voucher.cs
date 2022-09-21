using AccountsManager.Common.V1.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.DataModels.V1.Models
{
    public sealed class Voucher : BaseEntity
    {
        public VoucherType VoucherType { get; set; }
    }
}
