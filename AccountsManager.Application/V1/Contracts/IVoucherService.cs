using AccountsManager.ApplicationModels.V1.DTOs.VoucherDTOs;

namespace AccountsManager.Application.V1.Contracts
{
    public interface IVoucherService
    {
        Task<VoucherReadDTO> CreateVoucher(VoucherCreateDTO voucherCreateDTO);
        Task<List<VoucherReadDTO>> GetAllVouchers();
        Task<VoucherReadDTO> GetVoucherById(Guid id);
    }
}
