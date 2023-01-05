using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.ApplicationModels.V1.DTOs.TransactionsDTOs
{
    public sealed class TransactionCreateDTO
    {
        public Guid AccountId { get; set; }
        public decimal Debt { get; set; } = decimal.Zero;
        public decimal Credit { get; set; } = decimal.Zero;
    }
}
