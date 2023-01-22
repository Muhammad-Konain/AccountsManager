using AccountsManager.Application.V1.Contracts.HelperContracts;
using AccountsManager.Application.V1.Contracts.ServiceContracts;
using AccountsManager.ApplicationModels.V1.DTOs.TAccountDTOs;
using AccountsManager.ApplicationModels.V1.Exceptions;
using AccountsManager.Common.V1.Constants;
using AccountsManager.DataAccess.V1.Core;
using AccountsManager.DataModels.V1.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace AccountsManager.Application.V1.Services
{
    public sealed class TAccountService : ITAccountService
    {
        private IMappingExtension _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TAccountService(IMappingExtension mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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
                                                   .Take(Constants.DefaultPageSize)
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
    }
}
