using AccountsManager.Common.V1.Enums;

namespace AccountsManager.ApplicationModels.V1.DTOs.TAccountDTOs
{
    public sealed class TAccountCreateDTO
    {
        public string Title { get; set; }
        public AccountType AccountType { get; set; }
    }
}
