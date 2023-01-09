using AccountsManager.Application.V1.Contracts.HelperContracts;
using AccountsManager.Application.V1.Contracts.ServiceContracts;
using AccountsManager.Application.V1.Services;
using AccountsManager.ApplicationModels.V1.DTOs.TAccountDTOs;
using AccountsManager.ApplicationModels.V1.Exceptions;
using AccountsManager.Common.V1.Enums;
using AccountsManager.DataAccess.V1.Contracts;
using AccountsManager.DataAccess.V1.Core;
using AccountsManager.DataModels.V1.Models;
using Microsoft.EntityFrameworkCore.Update;
using MockQueryable.Moq;
using Moq;
using System.Security.Principal;

namespace AccountsManager.Test
{
    public class TAccountsTest
    {
        private readonly ITAccountService _accountService;
        private readonly Mock<IMappingExtension> _mapperMock = new();
        private readonly Mock<ITAccountRepository> _tAccountRepoMock = new();
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
            _unitOfWorkMock.Setup(s => s.AccountRepository.GetById(accountId))
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
            
            _unitOfWorkMock.Setup(s => s.AccountRepository.GetById(It.IsAny<Guid>()))
                           .Returns(() => Enumerable.Empty<TAccount>().AsQueryable().BuildMock());

            /// Act and  Assert
            await Assert.ThrowsAsync<EntityNotFoundExcetption>(() => _accountService.GetAccountById(accountId));
        }
        [Fact]
        public async Task DeleteAccount_ShouldThrowNotFoundException_WhenAccountDoesNotExist()
        {
            /// Arrange
            var accountId = Guid.NewGuid();
            _unitOfWorkMock.Setup(s => s.AccountRepository).Returns(_tAccountRepoMock.Object);

            _unitOfWorkMock.Setup(s => s.AccountRepository.GetById(It.IsAny<Guid>()))
                           .Returns(() => Enumerable.Empty<TAccount>().AsQueryable().BuildMock());

            /// Act and  Assert
            await Assert.ThrowsAsync<EntityNotFoundExcetption>(() => _accountService.DeleteAccount(accountId));
        }
        [Fact]
        public async Task DeleteAccount_ShouldDeleteAccount_WhenAccountExists()
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
            var deletedAccount = new TAccount
            {
                Id = accountId,
                Title = accountTitle,
                AccountType = accountType,
                IsActive = false
            };
            var entityResult = new List<TAccount> { accountEntity };

            _unitOfWorkMock.Setup(s => s.AccountRepository).Returns(_tAccountRepoMock.Object);
            _unitOfWorkMock.Setup(s => s.AccountRepository.GetById(accountId))
                           .Returns(() => entityResult.AsQueryable().BuildMock());
            _unitOfWorkMock.Setup(s => s.AccountRepository.Delete(accountEntity))
                           .Returns(deletedAccount);
            _unitOfWorkMock.Setup(s => s.SaveChangesAsync())
                           .ReturnsAsync(1);

            /// Act 
            var result = await _accountService.DeleteAccount(accountId);

            /// Assert
            Assert.Equal(1, result);
        }
        [Fact]
        public async Task CreateAccount_ShouldRetrunAccountReadDTO_WhenAccountIsCreated()
        {
            /// Arrange
            var accountId = Guid.NewGuid();
            var accountType = AccountType.Assets;
            var accountTitle = "Cash";
            var accountCreateDTO = new TAccountCreateDTO
            {
                Title = accountTitle,
                AccountType = accountType
            };
            var account = new TAccount
            {
                Id = accountId,
                Title = accountTitle,
                AccountType = accountType
            };
            var accountReadDTO = new TAccountReadDTO
            {
                Id = accountId,
                Title = accountTitle,
                AccountType = accountType
            };


            _unitOfWorkMock.Setup(s => s.AccountRepository).Returns(_tAccountRepoMock.Object);
            _mapperMock.Setup(s => s.MapEntity<TAccountCreateDTO, TAccount>(accountCreateDTO))
                       .Returns(account);
            _unitOfWorkMock.Setup(s => s.AccountRepository.CreateAsync(account))
                           .ReturnsAsync(account);
            _unitOfWorkMock.Setup(s => s.SaveChangesAsync())
                           .ReturnsAsync(1);
            _mapperMock.Setup(s => s.MapEntity<TAccount, TAccountReadDTO>(account))
                       .Returns(accountReadDTO);

            /// Act 
            var createdAccount = await _accountService.CreateAccount(accountCreateDTO);

            /// Assert
            Assert.Equal(accountId, createdAccount.Id);
            Assert.Equal(accountTitle, createdAccount.Title);
            Assert.Equal(accountType, createdAccount.AccountType);
        }
        [Fact]
        public async Task UpdateTAccount_ShouldThrowNotFoundException_WhenAccountDoesNotExist()
        {
            /// Arraange
            var accountId = Guid.NewGuid();
            var accountType = AccountType.Assets;
            var accountTitle = "Cash";
            var accountUpdateDTO = new TAccountUpdateDTO
            {
                Id=accountId,
                Title = accountTitle,
                AccountType = accountType
            };
            
            _unitOfWorkMock.Setup(s => s.AccountRepository).Returns(_tAccountRepoMock.Object);

            _unitOfWorkMock.Setup(s => s.AccountRepository.GetById(It.IsAny<Guid>()))
                           .Returns(() => Enumerable.Empty<TAccount>().AsQueryable().BuildMock());

            /// Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundExcetption>(() => _accountService.UpdateTAccount(accountUpdateDTO));

        }
        [Fact]
        public async Task UpdateTAccount_ShouldUpdateAccountandReturnReadDTO_WhenAccountExists()
        {
            /// Arraange
            var accountId = Guid.NewGuid();
            var accountType = AccountType.Assets;
            var accountTitle = "Cash";
            var accountUpdateDTO = new TAccountUpdateDTO
            {
                Id = accountId,
                Title = accountTitle,
                AccountType = accountType
            };
            var account = new TAccount
            {
                Id = accountId,
                Title = accountTitle,
                AccountType = accountType
            };
            var accountReadDTO = new TAccountReadDTO
            {
                Id = accountId,
                Title = accountTitle,
                AccountType = accountType
            };
            var entityResult = new List<TAccount> { account };


            _unitOfWorkMock.Setup(s => s.AccountRepository).Returns(_tAccountRepoMock.Object);
            _unitOfWorkMock.Setup(s => s.AccountRepository.GetById(accountId))
                           .Returns(()=> entityResult.AsQueryable().BuildMock());
            _mapperMock.Setup(s => s.MapEntiyInto(accountUpdateDTO, account))
                       .Returns(account);
            _unitOfWorkMock.Setup(s => s.AccountRepository.Update(account))
                           .Returns(account);
            _mapperMock.Setup(s => s.MapEntity<TAccount, TAccountReadDTO>(account))
                       .Returns(accountReadDTO);

            /// Act
            var result = await _accountService.UpdateTAccount(accountUpdateDTO);

            /// Assert 
            Assert.Equal(accountId, result.Id);
            Assert.Equal(accountTitle, result.Title);
            Assert.Equal(accountType, result.AccountType);
        }
    }
}
