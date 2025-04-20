using Azure;
using bill_payment.ImplementService;
using bill_payment.InterfacesService;
using bill_payment.Models.Admin;
using bill_payment.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bill_payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices userServices;
        public UsersController(IUserServices userServices)
        {
            this.userServices = userServices;
        }

        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserInput data)
        {

            var Response = await userServices.AddUser(data);
            if (Response.StatusCode == 200)
                return Ok(Response);
            else
                return BadRequest(Response);
        }
        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpPut]
        public async Task<IActionResult> EditUser(Guid id, [FromBody] UserInput data)
        {

            var Response = await userServices.EditUser(id, data);
            if (Response.StatusCode == 200)
                return Ok(Response);
            else
                return BadRequest(Response);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ListUsers([FromQuery] UserFilter filter)
        {
            var Response = await userServices.GetAllAsync(filter);
            return Ok(Response);
        }

        [Authorize]
        [HttpGet("export")]
        public async Task<IActionResult> ExportUsers([FromQuery] UserFilter filter)
        {
            var Response = await userServices.GetAllAsync(filter);

            var fileContent = await userServices.ExportUsers(Response.data);
            var fileName = $"Customers - {DateTime.UtcNow.ToString("yyyy-MM-dd")}";
            return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var Response = await userServices.DeleteUser(id);
            if (Response.StatusCode == 200)
                return Ok(Response);
            else
                return BadRequest(Response);
        }
        //[HttpGet("PartnerUsers")]
        //public async Task<IActionResult> GetUserByPartnerId(Guid Id)
        //{
        //    var Response = await userServices.GetUserByPartnerId(Id);
        //    if (Response.StatusCode == Enums.StatusCode.success.ToString())
        //        return Ok(Response);
        //    else
        //        return BadRequest(Response);
        //}
        [Authorize]
        [HttpGet("details")]
        public async Task<IActionResult> GetUserDetails(Guid Id)
        {
            var Response = await userServices.GetUserDetails(Id);
            if (Response.StatusCode == Enums.StatusCode.success.ToString())
                return Ok(Response);
            else
                return BadRequest(Response);
        }
    }
}
