using AccountsManager.Application.V1.Contracts.HelperContracts;
using AccountsManager.Application.V1.Contracts.ServiceContracts;
using AccountsManager.Application.V1.Services;
using AccountsManager.ApplicationModels.V1.DTOs.TransactionsDTOs;
using AccountsManager.ApplicationModels.V1.DTOs.VoucherDTOs;
using AccountsManager.ApplicationModels.V1.Exceptions;
using AccountsManager.Common.V1.Enums;
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
        private readonly Mock<IConfigReader> _configReaderMock = new();

        public VoucherTest()
        {
            _sut = new VoucherService(_mapperMock.Object, _unitOfWorkMock.Object, _configReaderMock.Object);
        }
        [Fact]
        public async Task CreateVoucher_ShouldThrowNotFoundException_WhenAnyAccountInTransacionsNotFound()
        {
            /// Arrange
            var description = "any string";
            var creditAccountId = Guid.NewGuid();
            var debtAccountId = Guid.NewGuid();

            var transaction = new List<TransactionCreateDTO> 
            {
                new TransactionCreateDTO() { AccountId = creditAccountId },
                new TransactionCreateDTO() { AccountId = debtAccountId }
            };
            var voucherCreateRequest = new VoucherCreateDTO
            {
                Description = description,
                Transactions = transaction
            };
            var accountIds = new List<Guid>() { creditAccountId, debtAccountId };
            var accountsFromDB = new List<TAccount>
            { 
                new TAccount() { Id = creditAccountId },
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
        [Fact]
        public async Task CreateVoucher_ShouldThrowInvalidBalanceException_WhenVoucherBalancesDoNotMatch()
        {
            /// Arrange
            var description = "any string";
            var creditAccountId = Guid.NewGuid();
            var debtAccountId = Guid.NewGuid();
            decimal creditAmount = 2000, debtAmount = 1000;
            
            var transaction = new List<TransactionCreateDTO> 
            {
                new TransactionCreateDTO() { AccountId = creditAccountId, Credit= creditAmount },
                new TransactionCreateDTO() { AccountId = debtAccountId, Debt = debtAmount }
            };
            var voucherCreateRequest = new VoucherCreateDTO
            {
                Description = description,
                Transactions = transaction
            };
            var accountIds = new List<Guid>() { creditAccountId, debtAccountId };
            var accountsFromDB = new List<TAccount>
            { 
                new TAccount() { Id = creditAccountId },
                new TAccount() { Id = debtAccountId }
            };

            _unitOfWorkMock.Setup(s => s.TransactionRepository)
                           .Returns(_transactionRepoMock.Object);
            _unitOfWorkMock.Setup(s => s.AccountRepository)
                           .Returns(_tAccountRepoMock.Object);

            _unitOfWorkMock.Setup(s => s.AccountRepository.GetAccounts(accountIds))
                           .Returns(() => accountsFromDB.AsQueryable().BuildMock());

            /// Act and Assert
            await Assert.ThrowsAsync<InvalidVoucherBalanceException>(() => _sut.CreateVoucher(voucherCreateRequest));
        }
        [Fact]
        public async Task CreateVoucher_ShouldCreateVoucher_WhenVoucherAllValidationsPass()
        {
            /// Arrange
            var description = "any string";
            var voucherId = Guid.NewGuid();
            var creditAccountId = Guid.NewGuid();
            var debtAccountId = Guid.NewGuid();
            decimal creditAmount, debtAmount;
            creditAmount = debtAmount = 1000;
            var voucherType = VoucherType.JournalVoucher;

            var transaction = new List<TransactionCreateDTO>
            {
                new TransactionCreateDTO() { AccountId = creditAccountId, Credit= creditAmount },
                new TransactionCreateDTO() { AccountId = debtAccountId, Debt = debtAmount }
            };
            var voucherCreateRequest = new VoucherCreateDTO
            {
                Description = description,
                Transactions = transaction
            };
            var accountIds = new List<Guid>() { creditAccountId, debtAccountId };
            var accountsFromDB = new List<TAccount>
            {
                new TAccount() { Id = creditAccountId },
                new TAccount() { Id = debtAccountId }
            };
            var voucher = new Voucher
            {
                Id = voucherId,
                VoucherType = voucherType,
                Transactions = new List<Transaction>
                {
                    new Transaction { AccountId = debtAccountId, Debt = debtAmount},
                    new Transaction { AccountId = creditAccountId, Credit = creditAmount}
                }
            };
            var voucherReadDTO = new VoucherReadDTO
            {
                Id = voucherId,
                VoucherType = voucherType,
                Transactions = new List<TransactionReadDTO>
                {
                    new TransactionReadDTO{ AccountId = creditAccountId, Credit = creditAmount },
                    new TransactionReadDTO{ AccountId = debtAccountId, Credit = debtAmount },
                }
            };

            _unitOfWorkMock.Setup(s => s.TransactionRepository)
                           .Returns(_transactionRepoMock.Object);
            _unitOfWorkMock.Setup(s => s.AccountRepository)
                           .Returns(_tAccountRepoMock.Object);
            _unitOfWorkMock.Setup(s => s.VoucherRepository)
                           .Returns(_voucherRepoMock.Object);

            _unitOfWorkMock.Setup(s => s.AccountRepository.GetAccounts(accountIds))
                           .Returns(() => accountsFromDB.AsQueryable().BuildMock());
            _mapperMock.Setup(s => s.MapEntity<VoucherCreateDTO, Voucher>(voucherCreateRequest))
                       .Returns(voucher);
            _unitOfWorkMock.Setup(s => s.VoucherRepository.CreateAsync(voucher))
                           .ReturnsAsync(voucher);
            _unitOfWorkMock.Setup(s => s.SaveChangesAsync())
                           .ReturnsAsync(1);
            _mapperMock.Setup(s => s.MapEntity<Voucher, VoucherReadDTO>(voucher))
                      .Returns(voucherReadDTO);


            /// Act 
            var result = await _sut.CreateVoucher(voucherCreateRequest);

            /// Assert
            Assert.Equal(voucherId, result.Id);
            Assert.Equal(voucherType, result.VoucherType);
            Assert.Equal(voucher.Transactions.Count, result.Transactions.Count);
        }
        [Fact]
        public async Task GetVoucherById_ShouldThroNotFoundExcetpion_IfVoucherDoesNotExist()
        {
            /// Arrange
            var voucherId = Guid.NewGuid();
            var emptyVouchers = new List<Voucher>();
            _unitOfWorkMock.Setup(s => s.VoucherRepository)
                           .Returns(_voucherRepoMock.Object);
            _unitOfWorkMock.Setup(s => s.VoucherRepository.GetById(It.IsAny<Guid>()))
                           .Returns(() => emptyVouchers.AsQueryable().BuildMock());

            /// Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundExcetption>(() => _sut.GetVoucherById(voucherId));
        }
        [Fact]
        public async Task GetVoucherById_ShouldReturnVoucher_IfVoucherExists()
        {
            /// Arrange
            var voucherId = Guid.NewGuid();
            var creditAccountId = Guid.NewGuid();
            var debtAccountId = Guid.NewGuid();
            var voucherType = VoucherType.JournalVoucher;
            
            var voucher = new  Voucher
            {
                Id = voucherId,
                VoucherType = voucherType,
                Transactions = new List<Transaction>
                {
                    new Transaction { AccountId = debtAccountId },
                    new Transaction { AccountId = creditAccountId }
                }
            };
            var voucherFromDB = new List<Voucher> { voucher };

            var voucherReadDTO = new VoucherReadDTO
            {
                Id = voucherId,
                VoucherType = voucherType,
                Transactions = new List<TransactionReadDTO>
                {
                    new TransactionReadDTO { AccountId = creditAccountId },
                    new TransactionReadDTO { AccountId = debtAccountId },
                }
            };

            _unitOfWorkMock.Setup(s => s.VoucherRepository)
                           .Returns(_voucherRepoMock.Object);
            _unitOfWorkMock.Setup(s => s.VoucherRepository.GetById(voucherId))
                           .Returns(() => voucherFromDB.AsQueryable().BuildMock());
            _mapperMock.Setup(s => s.MapEntity<Voucher, VoucherReadDTO>(voucher))
                    .Returns(voucherReadDTO);

            /// Act
            var result = await _sut.GetVoucherById(voucherId);

            /// Assert
            Assert.Equal(voucherId, result.Id);
            Assert.Equal(voucherType, result.VoucherType);
            Assert.Equal(voucher.Transactions.Count, result.Transactions.Count);
        }
        [Fact]
        public async Task DeleteVoucher_ShouldThrowNotFoundException_WhenVoucherDoesNotExist()
        {
            /// Arrange
            var voucherId = Guid.NewGuid();
            var emptyVouchers = new List<Voucher>();
            _unitOfWorkMock.Setup(s => s.VoucherRepository)
                           .Returns(_voucherRepoMock.Object);
            _unitOfWorkMock.Setup(s => s.VoucherRepository.GetById(It.IsAny<Guid>()))
                           .Returns(() => emptyVouchers.AsQueryable().BuildMock());

            /// Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundExcetption>(() => _sut.GetVoucherById(voucherId));
        }
        [Fact]
        public async Task DeleteVoucher_ShouldDeleteVoucher_WhenVoucherExists()
        {
            /// Arrange
            var voucherId = Guid.NewGuid();
            var creditAccountId = Guid.NewGuid();
            var debtAccountId = Guid.NewGuid();
            var voucherType = VoucherType.JournalVoucher;

            var voucher = new Voucher
            {
                Id = voucherId,
                VoucherType = voucherType,
                Transactions = new List<Transaction>
                {
                    new Transaction { AccountId = debtAccountId },
                    new Transaction { AccountId = creditAccountId }
                }
            };
            var voucherFromDB = new List<Voucher> { voucher };
            var deletedVoucher = new Voucher
            {
                Id = voucherId,
                VoucherType = voucherType,
                IsActive = false,
                Transactions = new List<Transaction>
                {
                    new Transaction { AccountId = debtAccountId, IsActive = false },
                    new Transaction { AccountId = creditAccountId, IsActive =false }
                }
            };

            _unitOfWorkMock.Setup(s => s.VoucherRepository)
                           .Returns(_voucherRepoMock.Object);
            _unitOfWorkMock.Setup(s => s.TransactionRepository)
                           .Returns(_transactionRepoMock.Object);
            _unitOfWorkMock.Setup(s => s.VoucherRepository.GetById(voucherId))
                           .Returns(() => voucherFromDB.AsQueryable().BuildMock());
            _unitOfWorkMock.Setup(s => s.VoucherRepository.Delete(voucher))
                           .Returns(deletedVoucher);
            _unitOfWorkMock.Setup(s => s.TransactionRepository.DeleteRange(voucher.Transactions.ToList()))
                           .Returns(deletedVoucher.Transactions.ToList());
            _unitOfWorkMock.Setup(s => s.SaveChangesAsync())
                           .ReturnsAsync(1);


            /// Act
            var result = await _sut.DeleteVoucher(voucherId);

            /// Assert 
            Assert.Equal(1, result);
        }
    }
}
