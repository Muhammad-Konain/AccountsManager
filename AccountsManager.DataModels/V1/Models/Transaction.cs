using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.DataModels.V1.Models
{
    public sealed class Transaction : BaseEntity
    {
        public decimal? Debt { get; set; }
        public decimal? Credit { get; set; }
        public Guid AccountId { get; set; }
        public TAccount Account { get; set; }
    }
}
