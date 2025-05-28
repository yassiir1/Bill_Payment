using bill_payment.MobileAppServices.Settings;
using bill_payment.Models.Partners;
using bill_payment.Models.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bill_payment.Controllers
{
    [Route("api/ui-settings")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsServices _settingsServices;
        public SettingsController(ISettingsServices settingsServices) 
        {
            _settingsServices = settingsServices;
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSettings( [FromBody] EditSettings data)
        {
            var Response = await _settingsServices.EditSettings( data);
            return Ok(Response);

        }

        [HttpGet]
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
        public async Task<IActionResult> AddBanner([FromForm] AddBanner data)
        {
            var Response = await _settingsServices.AddBanner(data);
            return Ok(Response);
        }
        [HttpPut("EditBanner/{id}")]
        public async Task<IActionResult> EditBanner(int id,[FromForm] AddBanner data)
        {
            var Response = await _settingsServices.EditBanner(id, data);
            return Ok(Response);
        }
        [HttpDelete("DeleteBanner/{id}")]
        public async Task<IActionResult> DeleteBanner(int id)
        {
            var Response = await _settingsServices.DeleteBanner(id);
            return Ok(Response);
        }
    }
}
