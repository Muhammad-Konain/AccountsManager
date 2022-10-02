using AccountsManager.Application.V1.Contracts;
using AccountsManager.Application.V1.Services;
using AccountsManager.ApplicationModels.V1.DTOs.TAccountDTOs;
using Microsoft.AspNetCore.Mvc;

namespace AccountsManager.API.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class TAccountsController : ControllerBase
    {
        private ITAccountService _accountService;

        public TAccountsController(ITAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTAccount(TAccountCreateDTO accountCreateDTO)
        {
            var created = await _accountService.CreateAccount(accountCreateDTO);
            return Ok(created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTAccount(TAccountUpdateDTO accountCreateDTO)
        {
            var created = await _accountService.UpdateTAccount(accountCreateDTO);
            return Ok(created);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(Guid id)
        {
            var created = await _accountService.GetAccountById(id); 
            return Ok(created);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var created = await _accountService.GetAllAccounts();
            return Ok(created);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            var restult = await _accountService.DeleteAccount(id);
            
            if(restult >= 0)
                return Ok();

            return BadRequest();
        }
    }
}
