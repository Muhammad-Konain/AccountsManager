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
        private readonly ITAccountService _accountService;
        private readonly ILogger<TAccountsController> _logger;

        public TAccountsController(ITAccountService accountService, ILogger<TAccountsController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTAccount(TAccountCreateDTO accountCreateDTO)
        {
            var created = await _accountService.CreateAccount(accountCreateDTO);
            return CreatedAtRoute(nameof(GetByID), new { id = created.Id }, created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTAccount(TAccountUpdateDTO accountCreateDTO)
        {
            var updated = await _accountService.UpdateTAccount(accountCreateDTO);
            return Ok(updated);
        }

        [HttpGet("{id}", Name= "GetByID")]
        public async Task<IActionResult> GetByID(Guid id)
        {
            _logger.LogInformation("logging");
            var account = await _accountService.GetAccountById(id); 
            return Ok(account);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllAccounts();
            return Ok(accounts);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            var deletedAccount = await _accountService.DeleteAccount(id);
            
            if(deletedAccount >= 0)
                return Ok();

            return BadRequest();
        }
    }
}
