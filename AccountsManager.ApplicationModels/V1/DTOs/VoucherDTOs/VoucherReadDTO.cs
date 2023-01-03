using AccountsManager.Common.V1.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.ApplicationModels.V1.DTOs.VoucherDTOs
{
    public sealed class VoucherReadDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public VoucherType VoucherType { get; set; }
    }
}
