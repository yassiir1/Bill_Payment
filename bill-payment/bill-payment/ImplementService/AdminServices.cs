﻿using Azure;
using Azure.Core;
using bill_payment.BillDbContext;
using bill_payment.Domains;
using bill_payment.InterfacesService;
using bill_payment.Models;
using bill_payment.Models.Admin;
using bill_payment.Models.Roles;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Net.Http.Headers;

namespace bill_payment.ImplementService
{
    public class AdminServices : IAdminServices
    {
        private readonly HttpClient _client;
        private readonly Bill_PaymentContext _billContext;
        private string BaseUrl = "http://localhost:8080";
        //private string BaseUrl = "http://127.0.0.1:8280/";

        public AdminServices(HttpClient client, Bill_PaymentContext billContext)
        {
            _client = client;
            _billContext = billContext;
        }
        public async Task<AddAdminResponse> AddAdmin(AdminInput data, string Token)
        {
            var Response = new AddAdminResponse();
            var createUserPayload = new
            {
                username = data.UserName,
                email = data.Email,
                enabled = true,
                firstName = data.FirstName,
                lastName = data.LastName,
                emailVerified = true,
                credentials = new[]
                        {
                new { type = "password", value = data.Password, temporary = false }
            }
            };
            var RoleResponse = _billContext.Roles.Where(c => c.RoleId == data.RoleId).FirstOrDefault();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            var createUserResponse = await _client.PostAsJsonAsync($"{BaseUrl}/auth/admin/realms/bill-payment-sdk/users", createUserPayload);

            if (!createUserResponse.IsSuccessStatusCode)
            {
                if(createUserResponse.ReasonPhrase == "Conflict")
                {
                    Response.StatusCode = 400;
                    Response.Message = "Error while Creating Admin - This Username is already exist";
                    return Response;
                }
                Response.StatusCode = 400;
                Response.Message = "Error while adding admin";
                return Response;
            }
            var AdminInput = new Admin()
            {
                Email = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName,
                UserName = data.UserName,
                Password = data.Password
            };
            await _billContext.Admins.AddAsync(AdminInput);
            await _billContext.SaveChangesAsync();
            var usersResponse = await _client.GetAsync($"{BaseUrl}/auth/admin/realms/bill-payment-sdk/users?username={data.UserName}");
            var users = await usersResponse.Content.ReadFromJsonAsync<List<UserRepresentation>>();
            var userId = users?.FirstOrDefault()?.Id;

            if (string.IsNullOrEmpty(userId))
            {
                Response.StatusCode = 400;
                Response.Message = "Error while adding admin";
                return Response;
            }
            var clientResponse = await _client.GetAsync($"{BaseUrl}/auth/admin/realms/bill-payment-sdk/clients?clientId=bill-payment-sdk-service");
            var AdminclientResponse = await _client.GetAsync($"{BaseUrl}/auth/admin/realms/bill-payment-sdk/clients?clientId=realm-management");

            var clients = await clientResponse.Content.ReadFromJsonAsync<List<ClientRepresentation>>();
            var Adminclients = await AdminclientResponse.Content.ReadFromJsonAsync<List<ClientRepresentation>>();
            var client = clients?.FirstOrDefault();
            var Adminclient = Adminclients?.FirstOrDefault();

            var roleResponse = await _client.GetAsync($"{BaseUrl}/auth/admin/realms/bill-payment-sdk/clients/{client?.Id}/roles/{RoleResponse?.RoleName}");
            var AdminclientRoles = await _client.GetAsync($"{BaseUrl}/auth/admin/realms/bill-payment-sdk/clients/{Adminclient?.Id}/roles");

            if (!roleResponse.IsSuccessStatusCode)

                if (string.IsNullOrEmpty(userId))
                {
                    Response.StatusCode = 400;
                    Response.Message = "Error while adding admin";
                    return Response;
                }
            var role = await roleResponse.Content.ReadFromJsonAsync<RoleRepresentation>();
            var AdminRoles = await AdminclientRoles.Content.ReadFromJsonAsync<List<RoleRepresentation>>();

            var jsonResponse = await AdminclientRoles.Content.ReadAsStringAsync();

            //var AdminRole = await AdminclientRoles.Content.ReadFromJsonAsync<RoleRepresentation>();
            //var ClientManagemntRoles = await ManageClientRoleResponse.Content.ReadFromJsonAsync<RoleRepresentation>();
            var assignRolePayload = new[] { new { id = role?.Id, name = role?.Name } };
            var assignRoleResponse = await _client.PostAsJsonAsync(
                $"{BaseUrl}/auth/admin/realms/bill-payment-sdk/users/{userId}/role-mappings/clients/{client?.Id}",
                assignRolePayload
            );
            var assignAdminRolePayload = new List<object>();
            for (int i = 0; i < AdminRoles.Count; i++)
            {
                assignAdminRolePayload.Add(new
                {
                    id = AdminRoles[i]?.Id,
                    name = AdminRoles[i]?.Name
                });
            }
            var assignAdminRoleResponse = await _client.PostAsJsonAsync(
                $"{BaseUrl}/auth/admin/realms/bill-payment-sdk/users/{userId}/role-mappings/clients/{Adminclient?.Id}",
                assignAdminRolePayload
            );

            Response.StatusCode = 200;
            Response.Message = "Admin Added Successfully";
            Response.data = new AdminDto()
            {
                Id = AdminInput.AdminId,
                Email = AdminInput.Email,
                FirstName = AdminInput.FirstName,
                LastName = AdminInput.LastName,
                UserName = AdminInput.UserName
            };
            return Response;
        }

        public async Task<AdminOutPut> GetAllAdmins()
        {
            var Response = new AdminOutPut();
            var data = await _billContext.Admins.ToListAsync();
            Response.StatusCode = 200;
            Response.Message = "Admin Returns Successfully";
            Response.data = data.Select(c=> new AdminDto
            {
                Email = c.Email,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Id = c.AdminId,
                UserName= c.UserName
            }).ToList();
            return Response;
        }
    }
}
