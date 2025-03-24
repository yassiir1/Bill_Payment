using bill_payment.InterfacesService;
using bill_payment.Models.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bill_payment.Controllers
{
    [Route("api/user/login")]
    [ApiController]
    public class MobileLoginController : ControllerBase
    {
        private readonly IUserLogin _userLogin;
        public MobileLoginController(IUserLogin userLogin)
        {
            _userLogin = userLogin;
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync()
        {
            var Response = await _userLogin.LoginAsync();
                return Ok(Response);
        }
    }
}
