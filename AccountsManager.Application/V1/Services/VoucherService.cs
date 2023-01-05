using AccountsManager.Application.V1.Contracts;
using AccountsManager.Application.V1.Helpers;
using AccountsManager.ApplicationModels.V1.DTOs.VoucherDTOs;
using AccountsManager.ApplicationModels.V1.Exceptions;
using AccountsManager.DataAccess.V1.Core;
using AccountsManager.DataModels.V1.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountsManager.Application.V1.Services
{
    public sealed class VoucherService : IVoucherService
    {
        private MappingHelper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public VoucherService(MappingHelper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<VoucherReadDTO> CreateVoucher(VoucherCreateDTO voucherCreateDTO)
        {
            var accounts = voucherCreateDTO.Transactions.Select(s => s.AccountId).ToList();
            var accountsFound = await _unitOfWork.AccountRepository.GetAccounts(accounts)
                                                                   .Select(s => s.Id)
                                                                   .ToListAsync();

            if (accounts.Count != accountsFound.Count)
                throw new EntityNotFoundExcetption(accounts.Except(accountsFound).ToList(), nameof(TAccount));

            var creditAmount = voucherCreateDTO.Transactions.Sum(s => s.Credit);
            var debtAmount = voucherCreateDTO.Transactions.Sum(s => s.Debt);

            if (creditAmount != debtAmount)
                throw new InvalidVoucherBalanceException(creditAmount, debtAmount);

            var voucher = _mapper.MapEntity<VoucherCreateDTO, Voucher>(voucherCreateDTO);
            await _unitOfWork.VoucherRepository.CreateAsync(voucher);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.MapEntity<Voucher, VoucherReadDTO>(voucher);
        }
        public async Task<List<VoucherReadDTO>> GetAllVouchers()
        {
            var vouchers = await _unitOfWork.VoucherRepository.GetAll()
                                            .AsNoTrackingWithIdentityResolution()
                                            .ToListAsync();

            return _mapper.MapEntity<List<Voucher>, List<VoucherReadDTO>>(vouchers);
        }
        public async Task<VoucherReadDTO> GetVoucherById(Guid id)
        {
            var voucher = await _unitOfWork.VoucherRepository.GetById(id)
                                     .AsNoTrackingWithIdentityResolution()
                                     .FirstOrDefaultAsync();

            if (voucher is null)
                throw new EntityNotFoundExcetption(id, nameof(voucher));

            return _mapper.MapEntity<Voucher, VoucherReadDTO>(voucher);
        }
        public async Task<int> DeleteVoucher(Guid id)
        {
            var voucher = await _unitOfWork.VoucherRepository.GetById(id)
                                                  .SingleOrDefaultAsync();

            if (voucher == null)
                throw new EntityNotFoundExcetption(id, nameof(voucher));

            _unitOfWork.VoucherRepository.Delete(voucher);
            
            if(voucher.Transactions != null)
                _unitOfWork.TransactionRepository.DeleteRange(voucher.Transactions);

            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
