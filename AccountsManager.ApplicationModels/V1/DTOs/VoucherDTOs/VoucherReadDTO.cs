using AccountsManager.ApplicationModels.V1.DTOs.TransactionsDTOs;
using AccountsManager.Common.V1.Enums;

namespace AccountsManager.ApplicationModels.V1.DTOs.VoucherDTOs
{
    public sealed class VoucherReadDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public VoucherType VoucherType { get; set; }
        public List<TransactionReadDTO> Transactions { get; set; } = new();
    }
}
