using bill_payment.ImplementService;
using bill_payment.InterfacesService;
using bill_payment.Models.Partners;
using bill_payment.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bill_payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnersController : ControllerBase
    {
        private readonly IPartnerServices _partnerServices;
        public PartnersController(IPartnerServices partnerServices)
        {
               _partnerServices = partnerServices;
        }
        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpPost]
        public async Task<IActionResult> AddPartner([FromBody] PartnerInput data)
        {

            var Response = await _partnerServices.AddPartner(data);
            if (Response.StatusCode == 200)
                return Ok(Response);
            else
                return BadRequest(Response);
        }
        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpPut]
        public async Task<IActionResult> EditPartner(Guid id, [FromBody] PartnerInput data)
        {

            var Response = await _partnerServices.EditPartner (id, data);
            if (Response.StatusCode == 200)
                return Ok(Response);
            else
                return BadRequest(Response);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ListPartners()
        {
            var Response = await _partnerServices.ListPartners();
            return Ok(Response);
        }
        [HttpGet("details")]
        public async Task<IActionResult> GetPartnerDetails(Guid Id)
        {
            var Response = await _partnerServices.GetPartnerDetails(Id);
            return Ok(Response);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteDelete(Guid id)
        {
            var Response = await _partnerServices.DeletePartner(id);
            if (Response.StatusCode == Enums.StatusCode.success.ToString())
                return Ok(Response);
            else
                return BadRequest(Response);
        }
    }
}
