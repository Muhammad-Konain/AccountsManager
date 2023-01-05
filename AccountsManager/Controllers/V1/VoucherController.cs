using AccountsManager.Application.V1.Contracts;
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
            return CreatedAtRoute(nameof(GetByID), new { id = created.Id }, created);
        }
        [HttpGet("{id}", Name = "GetVoucherById")]
        public async Task<IActionResult> GetByID(Guid id)
        {
            var voucher = await _voucherService.GetVoucherById(id);
            return Ok(voucher);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVouchers()
        {
            var vouchers = await _voucherService.GetAllVouchers();
            return Ok(vouchers);
        }
    }
}
