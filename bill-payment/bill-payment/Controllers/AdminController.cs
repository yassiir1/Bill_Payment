using bill_payment.Domains;
using bill_payment.ImplementService;
using bill_payment.InterfacesService;
using bill_payment.Models;
using bill_payment.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bill_payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices adminService;
        public AdminController(IAdminServices _adminService)
        {
            adminService = _adminService;
        }

        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpPost]
        public async Task<IActionResult> AddAdmin([FromBody] AdminInput data)
        {
            var user = HttpContext.User;
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            var TokenHeader = Request.Headers.TryGetValue("Authorization", out var authorizationHeader);
            var Token = authorizationHeader.ToString().Replace("Bearer ", string.Empty);

            var Response = await adminService.AddAdmin(data, Token);
            if (Response.StatusCode == 200)
                return Ok(Response);
            else
                return BadRequest(Response);
        }

        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditAdmin(Guid id,[FromBody] AdminEditInput data)
        {
            var user = HttpContext.User;
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            var TokenHeader = Request.Headers.TryGetValue("Authorization", out var authorizationHeader);
            var Token = authorizationHeader.ToString().Replace("Bearer ", string.Empty);

            var Response = await adminService.EditAdmin(id,data, Token);
            if (Response.StatusCode == 200)
                return Ok(Response);
            else
                return BadRequest(Response);
        }
        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(Guid id)
        {
            var user = HttpContext.User;
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            var TokenHeader = Request.Headers.TryGetValue("Authorization", out var authorizationHeader);
            var Token = authorizationHeader.ToString().Replace("Bearer ", string.Empty);

            var Response = await adminService.DeleteAdmin(id,Token);
            if (Response.StatusCode == 200)
                return Ok(Response);
            else
                return BadRequest(Response);
        }

        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpGet]
        public async Task<IActionResult> GetAllAdmins()
        {
            var Response = await adminService.GetAllAdmins();
            if (Response.StatusCode == 200)
                return Ok(Response);
            else
                return BadRequest(Response); ;
        }

        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdminDetail(Guid id)
        {
           
            var Response = await adminService.GetAdminDetails(id);
            if (Response?.StatusCode == 200)
                return Ok(Response);
            else
                return BadRequest(Response);
        }
    }
}
