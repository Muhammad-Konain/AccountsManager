using AccountsManager.ApplicationModels.V1.DTOs.PaginatedResponse;
using AccountsManager.ApplicationModels.V1.DTOs.VoucherDTOs;

namespace AccountsManager.Application.V1.Contracts.ServiceContracts
{
    public interface IVoucherService
    {
        Task<VoucherReadDTO> CreateVoucher(VoucherCreateDTO voucherCreateDTO);
        Task<List<VoucherReadDTO>> GetAllVouchers();
        Task<VoucherReadDTO> GetVoucherById(Guid id);
        Task<int> DeleteVoucher(Guid id);
        Task<PaginatedResponse<VoucherReadDTO>> GetAllVouchers(int pageNumber, int pageSize = default);
    }
}
