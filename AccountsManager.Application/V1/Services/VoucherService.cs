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
        public async Task CreateVoucher(VoucherCreateDTO voucherCreateDTO)
        {
            var accounts = voucherCreateDTO.Transactions.Select(s => s.AccountId).ToList();
            var accountsFound = await _unitOfWork.AccountRepository.Find(a => accounts.Contains(a.Id)).Select(s => s.Id).ToListAsync();

            if (accounts.Count != accountsFound.Count)
                throw new EntityNotFoundExcetption(accounts.Except(accountsFound).ToList(), nameof(TAccount));

            var creditAmount = voucherCreateDTO.Transactions.Sum(s => s.Credit);
            var debtAmount = voucherCreateDTO.Transactions.Sum(s => s.Debt);

            if (creditAmount != debtAmount)
                throw new InvalidVoucherBalanceException();
        }
    }
}
