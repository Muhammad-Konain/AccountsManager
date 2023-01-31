using AccountsManager.ApplicationModels.V1.DTOs.PaginatedResponse;
using AccountsManager.ApplicationModels.V1.DTOs.TAccountDTOs;

namespace AccountsManager.Application.V1.Contracts.ServiceContracts
{
    public interface ITAccountService
    {
        Task<TAccountReadDTO> CreateAccount(TAccountCreateDTO accountCreateDTO);
        Task<TAccountReadDTO> UpdateTAccount(TAccountUpdateDTO accountUpdateDTO);
        Task<IEnumerable<TAccountReadDTO>> GetAllAccounts();
        Task<TAccountReadDTO> GetAccountById(Guid id);
        Task<int> DeleteAccount(Guid id);
        Task<PaginatedResponse<TAccountReadDTO>> GetAllAccounts(int pageNumber, int pageSize = default);
    }
}