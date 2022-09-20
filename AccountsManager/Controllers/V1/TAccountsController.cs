using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountsManager.API.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class TAccountsController : ControllerBase
    {
        [HttpGet]

        public IActionResult get()
        {
            return Ok();
        }
    }
}
