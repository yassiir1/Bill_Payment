using bill_payment.ImplementService;
using bill_payment.InterfacesService;
using bill_payment.Models.Account;
using bill_payment.Models.Admin;
using bill_payment.Models.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bill_payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountServices;
        public AccountController(IAccountService accoutService)
        {
            _accountServices = accoutService;
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginInput admin)
        {

            var Response = await _accountServices.LoginAsync(admin);
            if (Response.StatusCode == 200)
                return Ok(Response);
            else
                return BadRequest(Response);
        }
    }
}
