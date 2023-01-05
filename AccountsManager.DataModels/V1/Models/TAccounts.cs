using AccountsManager.Common.V1.Enums;
using AccountsManager.DataModels.V1.Models;

namespace AccountsManager.DataModels.V1.Models
{
    public sealed class TAccount : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public AccountType AccountType { get; set; }
    }
}
