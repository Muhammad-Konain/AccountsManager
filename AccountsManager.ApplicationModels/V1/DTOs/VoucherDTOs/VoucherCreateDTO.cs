using AccountsManager.ApplicationModels.V1.DTOs.TransactionsDTOs;
using AccountsManager.Common.V1.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.ApplicationModels.V1.DTOs.VoucherDTOs
{
    public sealed class VoucherCreateDTO
    {
        public VoucherType VoucherType { get; set; }
        public List<TransactionCreateDTO> Transactions { get; set; } = new();
    }
}
