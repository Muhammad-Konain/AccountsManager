using AccountsManager.ApplicationModels.V1.DTOs.BaseDTOs;
using AccountsManager.Common.V1.Enums;

namespace AccountsManager.ApplicationModels.V1.DTOs.TAccountDTOs
{
    public sealed class TAccountReadDTO : BaseReadDTO
    {
        public string Title { get; set; }
        public AccountType AccountType { get; set; }
    }
}
