﻿using AccountsManager.Application.V1.Contracts.HelperContracts;
using AccountsManager.Application.V1.Contracts.ServiceContracts;
using AccountsManager.Application.V1.Services;
using AccountsManager.ApplicationModels.V1.DTOs.TransactionsDTOs;
using AccountsManager.ApplicationModels.V1.DTOs.VoucherDTOs;
using AccountsManager.ApplicationModels.V1.Exceptions;
using AccountsManager.DataAccess.V1.Contracts;
using AccountsManager.DataAccess.V1.Core;
using AccountsManager.DataModels.V1.Models;
using MockQueryable.Moq;
using Moq;

namespace AccountsManager.Test
{
    public class VoucherTest
    {
        private readonly IVoucherService _sut;
        private readonly Mock<IMappingExtension> _mapperMock = new();
        private readonly Mock<IVoucherRepository> _voucherRepoMock = new();
        private readonly Mock<ITransactionRepository> _transactionRepoMock = new();
        private readonly Mock<ITAccountRepository> _tAccountRepoMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

        public VoucherTest()
        {
            _sut = new VoucherService(_mapperMock.Object, _unitOfWorkMock.Object);
        }
        [Fact]
        public async Task CreateVoucher_ShouldThrowNotFoundException_WhenAnyAccountInTransacionsNotFound()
        {
            /// Arrange
            var description = "any string";
            var accountId1 = Guid.NewGuid();
            var accountId2 = Guid.NewGuid();

            var transaction = new List<TransactionCreateDTO> 
            {
                new TransactionCreateDTO() { AccountId = accountId1 },
                new TransactionCreateDTO() { AccountId = accountId2 }
            };
            var voucherCreateRequest = new VoucherCreateDTO
            {
                Description = description,
                Transactions = transaction
            };
            var accountIds = new List<Guid>() { accountId1, accountId2 };
            var accountsFromDB = new List<TAccount>
            { 
                new TAccount() { Id = accountId1 },
            };

            _unitOfWorkMock.Setup(s => s.TransactionRepository)
                           .Returns(_transactionRepoMock.Object);
            _unitOfWorkMock.Setup(s => s.AccountRepository)
                           .Returns(_tAccountRepoMock.Object);

            _unitOfWorkMock.Setup(s => s.AccountRepository.GetAccounts(accountIds))
                           .Returns(() => accountsFromDB.AsQueryable().BuildMock());

            /// Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundExcetption>(() => _sut.CreateVoucher(voucherCreateRequest));
        }
    }
}
