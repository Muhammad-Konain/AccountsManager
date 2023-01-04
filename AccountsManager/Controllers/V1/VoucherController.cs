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
            this._voucherService = voucherService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTAccount(VoucherCreateDTO voucherCreateDTO)
        {
            var result = await _voucherService.CreateVoucher(voucherCreateDTO);
            return Ok(result);
        }
    }
}
