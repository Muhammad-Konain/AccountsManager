using AccountsManager.Application.V1.Contracts.HelperContracts;
using AccountsManager.Application.V1.Contracts.ServiceContracts;
using AccountsManager.ApplicationModels.V1.DTOs.PaginatedResponse;
using AccountsManager.ApplicationModels.V1.DTOs.VoucherDTOs;
using AccountsManager.ApplicationModels.V1.Exceptions;
using AccountsManager.Common.V1.Constants;
using AccountsManager.DataAccess.V1.Core;
using AccountsManager.DataModels.V1.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountsManager.Application.V1.Services
{
    public sealed class VoucherService : IVoucherService
    {
        private readonly IMappingExtension _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfigReader _configReader;
        public VoucherService(IMappingExtension mapper, IUnitOfWork unitOfWork, IConfigReader configReader)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configReader = configReader;
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
        public async Task<PaginatedResponse<VoucherReadDTO>> GetAllVouchers(int pageNumber, int pageSize = default)
        {
            if(pageNumber <= 0)
                throw new ArgumentException($"Invalid page number: {pageNumber}");

            if (pageSize <= 0)
                throw new ArgumentException($"Invalid page size: {pageSize}");

            if(pageSize == default)
                pageSize = _configReader.GetSectionValue<int>(Constants.DefaultPageSize);

            var vouchers = await _unitOfWork.VoucherRepository.GetVouchers(pageNumber, pageSize).ToListAsync();
            var totalVouchers = await _unitOfWork.VoucherRepository.GetAll().CountAsync();

            var resultSet = _mapper.MapEntity<List<Voucher>, List<VoucherReadDTO>>(vouchers);

            return new PaginatedResponse<VoucherReadDTO>
            {
                Data = resultSet,
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalEntities = totalVouchers,
            };
        }
    }
}
