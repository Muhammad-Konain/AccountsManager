using AccountsManager.Common.V1.Enums;

namespace AccountsManager.DataModels.V1.Models
{
    public sealed class Voucher : BaseEntity
    {
        public VoucherType VoucherType { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
    }
}