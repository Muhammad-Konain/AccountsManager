using AccountsManager.Common.V1.Enums;

namespace AccountsManager.ApplicationModels.V1.DTOs.TAccountDTOs
{
    public sealed class TAccountReadDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public AccountType AccountType { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
    }
}
