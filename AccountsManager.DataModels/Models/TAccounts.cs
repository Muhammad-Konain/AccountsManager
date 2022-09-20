using AccountsManager.Common.Enums;

namespace AccountsManager.DataModels.Models
{
    internal class TAccount : BaseEntity
    {
        public string Title { get; set; }
        public AccountType AccountType { get; set; }
    }
}
