using AccountsManager.Application.V1.Helpers;
using AccountsManager.ApplicationModels.V1.DTOs.TAccountDTOs;
using AccountsManager.DataAccess.V1.Contracts;
using AccountsManager.DataModels.V1.Models;

namespace AccountsManager.Application.V1.Services
{
    public sealed class TAccountService
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

            return _mapper.MapEntity<TAccount, TAccountReadDTO>(accountModel);
        }
    }
}
