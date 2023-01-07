using AccountsManager.Application.V1.Contracts.HelperContracts;
using AccountsManager.Application.V1.Contracts.ServiceContracts;
using AccountsManager.Application.V1.Services;
using AccountsManager.ApplicationModels.V1.DTOs.TAccountDTOs;
using AccountsManager.ApplicationModels.V1.Exceptions;
using AccountsManager.Common.V1.Enums;
using AccountsManager.DataAccess.V1.Contracts;
using AccountsManager.DataAccess.V1.Core;
using AccountsManager.DataModels.V1.Models;
using MockQueryable.Moq;
using Moq;

namespace AccountsManager.Test
{
    public class TAccountsTest
    {
        private readonly ITAccountService _accountService;
        private readonly Mock<IMappingExtension> _mapperMock = new();
        private readonly Mock<ITAccountRepository> _tAccountRepoMock = new();
        private readonly Mock<ITransactionRepository> _transactionRepoMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

        public TAccountsTest()
        {
            _accountService = new TAccountService(_mapperMock.Object, _unitOfWorkMock.Object);
        }
        [Fact]
        public async Task GetAccountById_ShouldReturnAccount_WhenAccountWithIdIsPresent()
        {
            /// Arrange
            var accountId = Guid.NewGuid();
            var accountType = AccountType.Assets;
            var accountTitle = "Cash";
            var accountEntity = new TAccount
            {
                Id = accountId,
                Title = accountTitle,
                AccountType = accountType
            };
            var entityResult = new List<TAccount> { accountEntity };
            var accountReadDTO = new TAccountReadDTO
            {
                Id = accountId,
                Title = accountTitle,
                AccountType = accountType
            };

            _unitOfWorkMock.Setup(s => s.AccountRepository).Returns(_tAccountRepoMock.Object);
            _unitOfWorkMock.Setup(fu => fu.AccountRepository.GetById(accountId))
                           .Returns(() => entityResult.AsQueryable() .BuildMock());
            _mapperMock.Setup(s => s.MapEntity<TAccount, TAccountReadDTO>(accountEntity))
                       .Returns(accountReadDTO);


            /// Act 
            var account = await _accountService.GetAccountById(accountId);

            /// Assert
            Assert.Equal(accountId, account.Id);
            Assert.Equal(accountTitle, account.Title);
            Assert.Equal(accountType, account.AccountType);
        }
        [Fact]
        public async Task GetAccountById_ShouldTrowNotFoundExcception_WhenAccountIsNotFound()
        {
            /// Arrange
            var accountId = Guid.NewGuid();
            _unitOfWorkMock.Setup(s => s.AccountRepository).Returns(_tAccountRepoMock.Object);
            
            _unitOfWorkMock.Setup(fu => fu.AccountRepository.GetById(It.IsAny<Guid>()))
                           .Returns(() => Enumerable.Empty<TAccount>().AsQueryable().BuildMock());

            /// Act and  Assert
            await Assert.ThrowsAsync<EntityNotFoundExcetption>(() => _accountService.GetAccountById(accountId));
        }
    }
}
