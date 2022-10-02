using AccountsManager.Application.V1.Contracts;
using AccountsManager.Application.V1.Helpers;
using AccountsManager.ApplicationModels.V1.DTOs.TAccountDTOs;
using AccountsManager.ApplicationModels.V1.Exceptions;
using AccountsManager.DataAccess.V1.Contracts;
using AccountsManager.DataModels.V1.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountsManager.Application.V1.Services
{
    public sealed class TAccountService : ITAccountService
    {
        private MappingHelper _mapper;
        private ITAccountRepository _accountRepository;

        public TAccountService(MappingHelper mapper, ITAccountRepository accountRepository)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
        }
        public async Task<TAccountReadDTO> CreateAccount(TAccountCreateDTO accountCreateDTO)
        {
            var accountModel = _mapper.MapEntity<TAccountCreateDTO, TAccount>(accountCreateDTO);
            await _accountRepository.Create(accountModel);
            await _accountRepository.Save();
            
            return _mapper.MapEntity<TAccount, TAccountReadDTO>(accountModel);
        }

        public async Task<TAccountReadDTO> UpdateTAccount(TAccountUpdateDTO accountUpdateDTO)
        {
            var accountInDB =await _accountRepository.GetById(accountUpdateDTO.Id)
                                                     .FirstOrDefaultAsync();
            
            if (accountInDB == null)
                throw new EntityNotFoundExcetption(accountUpdateDTO.Id);

            _mapper.MapEntiyInto(accountUpdateDTO, accountInDB);

            _accountRepository.Update(accountInDB);
            await _accountRepository.Save();
         
            return _mapper.MapEntity<TAccount, TAccountReadDTO>(accountInDB);
        }

        public async Task<IEnumerable<TAccountReadDTO>> GetAllAccounts()
        {
            var accoutns = await _accountRepository.GetAll()
                                                   .AsNoTrackingWithIdentityResolution()
                                                   .ToListAsync();

            return _mapper.MapEntity<List<TAccount>, List<TAccountReadDTO>>(accoutns);
        }
        public async Task<TAccountReadDTO> GetAccountById(Guid id)
        {
            var account = await _accountRepository.GetById(id)
                                                  .AsNoTrackingWithIdentityResolution()
                                                  .SingleOrDefaultAsync();
            if (account == null)
                throw new EntityNotFoundExcetption(id);

            return _mapper.MapEntity<TAccount, TAccountReadDTO>(account);
        }
        public async Task<int> DeleteAccount(Guid id)
        {
            var account = await _accountRepository.GetById(id)
                                                  .SingleOrDefaultAsync();
            
            if (account == null)
                throw new EntityNotFoundExcetption(id);

            _accountRepository.Delete(account);

            return await _accountRepository.Save();
        }
    }
}
