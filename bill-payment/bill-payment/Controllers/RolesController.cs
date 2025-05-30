﻿using bill_payment.Domains;
using bill_payment.InterfacesService;
using bill_payment.Models.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace bill_payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesServices rolesServices;
        public RolesController(IRolesServices rolesServices)
        {
            this.rolesServices = rolesServices;
        }

        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] RolesInput role)
        {
            var user = HttpContext.User;
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            var TokenHeader = Request.Headers.TryGetValue("Authorization", out var authorizationHeader);
            var Token = authorizationHeader.ToString().Replace("Bearer ", string.Empty);

            var Response = await rolesServices.AddNewRole(role, Token);
            if (Response.StatusCode == 200)
                return Ok(Response);
            else
                return BadRequest(Response);
        }

        [Authorize]
        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var Response = await rolesServices.GetAllRoles();
            if (Response.StatusCode == 200)
                return Ok(Response);
            else
                return BadRequest(Response);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> SoftDeleteRole(Guid id)
        {
            var Response = await rolesServices.SoftDeleteRole(id);
            if (Response.StatusCode == 200)
                return Ok(Response);
            else
                return BadRequest(Response);
        }
    }
}
