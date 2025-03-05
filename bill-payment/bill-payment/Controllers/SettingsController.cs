using bill_payment.MobileAppServices.Settings;
using bill_payment.Models.Partners;
using bill_payment.Models.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bill_payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsServices _settingsServices;
        public SettingsController(ISettingsServices settingsServices) 
        {
            _settingsServices = settingsServices;
        }
        [HttpPut("UpdateSettings")]
        public async Task<IActionResult> UpdateSettings( [FromBody] EditSettings data)
        {
            var Response = await _settingsServices.EditSettings( data);
            return Ok(Response);

        }

        [HttpGet("GetSettings")]
        public async Task<IActionResult> ListSettings()
        {
            var Response = await _settingsServices.ListSettings();
            return Ok(Response);
        }
        [HttpGet("ListBanners")]
        public async Task<IActionResult> ListBanners()
        {
            var Response = await _settingsServices.ListBanners();
            return Ok(Response);
        }
        [HttpPost("AddBanner")]
        public async Task<IActionResult> AddBanner(AddBanner data)
        {
            var Response = await _settingsServices.AddBanner(data);
            return Ok(Response);
        }
    }
}
