using AccountsManager.Application.V1.Contracts.ServiceContracts;
using AccountsManager.ApplicationModels.V1.DTOs.VoucherDTOs;
using Microsoft.AspNetCore.Mvc;

namespace AccountsManager.API.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public sealed class VoucherController : ControllerBase
    {
        private readonly IVoucherService _voucherService;

        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTAccount(VoucherCreateDTO voucherCreateDTO)
        {
            var created = await _voucherService.CreateVoucher(voucherCreateDTO);
            return CreatedAtRoute(nameof(GetVoucherById), new { id = created.Id }, created);
        }
        [HttpGet("{id}", Name = "GetVoucherById")]
        public async Task<IActionResult> GetVoucherById(Guid id)
        {
            var voucher = await _voucherService.GetVoucherById(id);
            return Ok(voucher);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllVouchers(int pageNumber, int pageSize = 0)
        {
            var vouchers = await _voucherService.GetAllVouchers(pageNumber, pageSize);
            return Ok(vouchers);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            var deletedAccount = await _voucherService.DeleteVoucher(id);

            if (deletedAccount >= 0)
                return Ok();

            return BadRequest();
        }
    }
}
