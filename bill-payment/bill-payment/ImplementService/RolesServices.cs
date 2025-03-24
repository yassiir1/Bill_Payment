using Azure.Core;
using bill_payment.BillDbContext;
using bill_payment.Domains;
using bill_payment.InterfacesService;
using bill_payment.Models.Roles;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace bill_payment.ImplementService
{
    public class RolesServices : IRolesServices
    {
        private readonly Bill_PaymentContext _billContext;
        private readonly IHttpClientFactory _clientFactory;
        private string BaseUrl = "http://localhost:8080";
        //private string BaseUrl = "http://127.0.0.1:8280";


        public RolesServices(Bill_PaymentContext billContext, IHttpClientFactory clientFactory)
        {
            _billContext = billContext;
            _clientFactory = clientFactory;
        }
        public async Task<AddRoleResponse> AddNewRole(RolesInput data, string Token)
        {
            var model = new Roles
            {
                RoleDescription = data.RoleDescription,
                RoleName = data.RoleName,
            };
            var ReturnModel = new AddRoleResponse();
            try
            {
                var ExistRole = _billContext.Roles.Where(c => c.RoleName == data.RoleName).FirstOrDefault();
                
     
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                    client.BaseAddress = new Uri($"{BaseUrl}/auth/admin/realms/bill-payment-sdk/");

                    var role = new { name = model.RoleName, description = model.RoleDescription };

                    var response = await client.PostAsJsonAsync("roles", role);

                    if (!response.IsSuccessStatusCode)
                    {
                        if(response.ReasonPhrase == "Conflict")
                        {
                            ReturnModel.Message = "Error During Inserting Roles - Role Name is Already Exist";
                            ReturnModel.StatusCode = 400;
                            ReturnModel.Roles = new Roles();
                            return ReturnModel;
                        }
                        ReturnModel.Message = "Error During Inserting Roles";
                        ReturnModel.StatusCode = 400;
                        ReturnModel.Roles = new Roles();
                        return ReturnModel;

                    }
                    if (ExistRole == null)
                    {
                        await _billContext.Roles.AddAsync(model);
                        await _billContext.SaveChangesAsync();
                    }
                }

                ReturnModel.Message = "Roles Inserted Successfully";
                ReturnModel.StatusCode = 200;
                ReturnModel.Roles = model;
            }
            catch (Exception ex) {
                ReturnModel.Message = "Error During Inserting Roles";
                ReturnModel.StatusCode = 400;
                ReturnModel.Roles = new Roles();
            }
            return ReturnModel;
        }

        public async Task<RolesResponse> GetAllRoles()
        {
            var ReturnModel = new RolesResponse();
            try
            {
                var Roles = await _billContext.Roles.ToListAsync();
                ReturnModel.Message = "Roles Returned Successfully";
                ReturnModel.StatusCode = 200;
                ReturnModel.data = Roles;
            }
            catch(Exception ex) 
            {
                ReturnModel.Message = "Error on Returning Roles List";
                ReturnModel.StatusCode = 400;
                ReturnModel.data = new List<Roles>();
            }
            return ReturnModel;
        }

        public async Task<DeleteRoleResponse> SoftDeleteRole(Guid Id)
        {
            var ReturnModel = new DeleteRoleResponse();
            try
            {
                var Role = await _billContext.Roles.Where(c => c.RoleId == Id).FirstOrDefaultAsync();
                if (Role == null)
                    ReturnModel.Message = "No Role founded!";
                else
                {
                    _billContext.Roles.Remove(Role);
                    await _billContext.SaveChangesAsync();
                    ReturnModel.Message = "Role Deleted Successfully!";
                }
                ReturnModel.StatusCode = 200;
                return ReturnModel;
            }
            catch (Exception ex) {
                ReturnModel.Message = "Error During Removing Role!";
                ReturnModel.StatusCode = 400;
                return ReturnModel;
            }
        }
    }
}
