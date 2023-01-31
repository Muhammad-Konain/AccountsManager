using AccountsManager.Application.V1.Contracts.HelperContracts;
using AccountsManager.Application.V1.Contracts.ServiceContracts;
using AccountsManager.ApplicationModels.V1.DTOs.PaginatedResponse;
using AccountsManager.ApplicationModels.V1.DTOs.TAccountDTOs;
using AccountsManager.ApplicationModels.V1.Exceptions;
using AccountsManager.Common.V1.Constants;
using AccountsManager.DataAccess.V1.Core;
using AccountsManager.DataModels.V1.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountsManager.Application.V1.Services
{
    public sealed class TAccountService : ITAccountService
    {
        private readonly IMappingExtension _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfigReader _configReader;

        public TAccountService(IMappingExtension mapper, IUnitOfWork unitOfWork, IConfigReader configReader)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configReader = configReader;
        }
        public async Task<TAccountReadDTO> CreateAccount(TAccountCreateDTO accountCreateDTO)
        {
            var accountModel = _mapper.MapEntity<TAccountCreateDTO, TAccount>(accountCreateDTO);
            await _unitOfWork.AccountRepository.CreateAsync(accountModel);
            await _unitOfWork.SaveChangesAsync();
            
            return _mapper.MapEntity<TAccount, TAccountReadDTO>(accountModel);
        }
        public async Task<TAccountReadDTO> UpdateTAccount(TAccountUpdateDTO accountUpdateDTO)
        {
            var accountInDB =await _unitOfWork.AccountRepository.GetById(accountUpdateDTO.Id)
                                                     .FirstOrDefaultAsync();
            
            if (accountInDB == null)
                throw new EntityNotFoundExcetption(accountUpdateDTO.Id, nameof(TAccount));

            _mapper.MapEntiyInto(accountUpdateDTO, accountInDB);

            _unitOfWork.AccountRepository.Update(accountInDB);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.MapEntity<TAccount, TAccountReadDTO>(accountInDB);
        }
        public async Task<IEnumerable<TAccountReadDTO>> GetAllAccounts()
        {
            var accounts = await _unitOfWork.AccountRepository.GetAll()
                                                   .AsNoTrackingWithIdentityResolution()
                                                   .ToListAsync();

            return _mapper.MapEntity<List<TAccount>, List<TAccountReadDTO>>(accounts);
        }
        public async Task<TAccountReadDTO> GetAccountById(Guid id)
        {
            var account = await _unitOfWork.AccountRepository.GetById(id)
                                                  .AsNoTrackingWithIdentityResolution()
                                                  .SingleOrDefaultAsync();
            if (account == null)
                throw new EntityNotFoundExcetption(id, nameof(TAccount));

            return _mapper.MapEntity<TAccount, TAccountReadDTO>(account);
        }
        public async Task<int> DeleteAccount(Guid id)
        {
            var account = await _unitOfWork.AccountRepository.GetById(id)
                                                  .SingleOrDefaultAsync();
            
            if (account == null)
                throw new EntityNotFoundExcetption(id);

            _unitOfWork.AccountRepository.Delete(account);

            return await _unitOfWork.SaveChangesAsync();
        }
        public async Task<PaginatedResponse<TAccountReadDTO>> GetAllAccounts(int pageNumber, int pageSize = default)
        {
            if (pageNumber < 0)
                throw new ArgumentException($"Invalid page number: {pageNumber}");

            if (pageSize == default)
                pageSize = _configReader.GetSectionValue<int>(Constants.DefaultPageSize);

            var accounts = await _unitOfWork.AccountRepository.GetAccounts(pageNumber, pageSize).ToListAsync();
            var totalAccounts = await _unitOfWork.AccountRepository.GetAll().CountAsync();

            var resultSet = _mapper.MapEntity<List<TAccount>, List<TAccountReadDTO>>(accounts);

            return new PaginatedResponse<TAccountReadDTO>
            {
                Data = resultSet,
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalEntities = totalAccounts,
            };
        }
    }
}
