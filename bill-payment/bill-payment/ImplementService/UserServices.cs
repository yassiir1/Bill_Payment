using bill_payment.BillDbContext;
using bill_payment.Domains;
using bill_payment.Enums;
using bill_payment.InterfacesService;
using bill_payment.Models.Partners;
using bill_payment.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace bill_payment.ImplementService
{
    public class UserServices : IUserServices
    {
        private readonly Bill_PaymentContext _billContext;
        public UserServices(Bill_PaymentContext billContext)
        {
            _billContext = billContext;
        }
        public async Task<AddUserResponse> AddUser(UserInput model)
        {
            var Response = new AddUserResponse();
            try
            {
                var data = new User
                {
                    CreationDate = DateTime.UtcNow,
                    DateOfBirth = DateTime.SpecifyKind(model.DateOfBirth, DateTimeKind.Utc),
                    FullName = model.FullName,
                    Gender = model.Gender,
                    NationalId = model.NationalId,
                    PhoneNumber = model.PhoneNumber,
                };

                await _billContext.Users.AddAsync(data);
                await _billContext.SaveChangesAsync();
                var UserPartnerData = new UserPartners
                {
                    PartnerId = model.PartnerId.Value,
                    UserId = data.UserId,
                };
                await _billContext.UserPartners.AddAsync(UserPartnerData);
                await _billContext.SaveChangesAsync();
                Response.Message = "User Added Successfully";
                Response.StatusCode = 200;
                Response.data = new ListUsersOutPut()
                {
                    CreationDate = data.CreationDate,
                    DateOfBirth = data.DateOfBirth,
                    FullName = data.FullName,
                    Gender = data.Gender,
                    NationalId = data.NationalId,
                    PhoneNumber = data.PhoneNumber,
                    UserId = data.UserId
                };

            }
            catch (Exception ex)
            {
                Response.Message = "Error While Creating User";
                Response.StatusCode = 400;
            }
            return Response;
        }

        public async Task<DeleteUserResponse> DeleteUser(Guid id)
        {
            var Response = new DeleteUserResponse();
            var data = await _billContext.Users.Where(c => c.UserId == id).FirstOrDefaultAsync();
            if (data == null)
            {
                Response.StatusCode = 400;
                Response.Message = "User Not Founded!";
                return Response;
            }
            _billContext.Users.Remove(data);
            await _billContext.SaveChangesAsync();
            Response.StatusCode = 200;
            Response.Message = "User Deleted Successfully!";
            return Response;
        }

        public async Task<AddUserResponse> EditUser(Guid id, UserInput model)
        {
            var Response = new AddUserResponse();
            var data = await _billContext.Users.Where(c => c.UserId == id).FirstOrDefaultAsync();
            if (data == null)
            {
                Response.StatusCode = 400;
                Response.Message = "There is no User With This Id";
                return Response;
            }
            try
            {
                data.FullName = model.FullName;
                data.PhoneNumber = model.PhoneNumber;
                data.NationalId = model.NationalId;
                data.DateOfBirth = model.DateOfBirth;
                data.Gender = model.Gender;
                if (data.PartnerId != model.PartnerId)
                {
                    data.PartnerId = model.PartnerId;
                    var UserPartner = await _billContext.UserPartners.Where(c => c.UserId == data.UserId && c.PartnerId == data.PartnerId).FirstOrDefaultAsync();
                    if (UserPartner == null)
                    {
                        var UserPartnerInput = new UserPartners
                        {
                            UserId = data.UserId,
                            PartnerId = model.PartnerId.Value,
                        };
                        await _billContext.UserPartners.AddAsync(UserPartnerInput);
                        await _billContext.SaveChangesAsync();
                    }
                }
                Response.StatusCode = 200;
                Response.data = new ListUsersOutPut()
                {
                    CreationDate = data.CreationDate,
                    DateOfBirth = data.DateOfBirth,
                    FullName = data.FullName,
                    Gender = data.Gender,
                    NationalId = data.NationalId,
                    PhoneNumber = data.PhoneNumber,
                    UserId = data.UserId
                };
                Response.Message = "User Modified Successfully";
                return Response;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                Response.Message = "Error While Modifying User";
                return Response;
            }
        }
        public async Task<ListUserResponse> GetAllAsync()
        {
            var Response = new ListUserResponse();
            var data = await _billContext.Users.ToListAsync();
            Response.StatusCode = StatusCode.success.ToString();
            Response.Message = "Data Returned successfully";
            Response.data = data.Select(c => new ListUsersOutPut()
            {
                NationalId = c.NationalId,
                UserId = c.UserId,
                CreationDate = c.CreationDate,
                DateOfBirth = c.DateOfBirth,
                FullName = c.FullName,
                Gender = c.Gender,
                PhoneNumber = c.PhoneNumber
            }).ToList();
            return Response;

        }

        public async Task<ListUserResponse> GetUserByPartnerId(Guid id)
        {
            var Response = new ListUserResponse();

            var data = await _billContext.Users.Where(c => c.PartnerId == id).ToListAsync();
            if (data.Count == 0)
            {
                Response.StatusCode = StatusCode.error.ToString();
                Response.Message = "There are no Customers with this partner";
                return Response;
            }
            Response.StatusCode = StatusCode.success.ToString();
            Response.Message = "Data Returned Successfully";
            Response.data.Select(c => new ListUsersOutPut()
            {
                CreationDate = c.CreationDate,
                DateOfBirth = c.DateOfBirth,
                FullName = c.FullName,
                Gender = c.Gender,
                NationalId = c.NationalId,
                PhoneNumber = c.PhoneNumber,
                UserId = c.UserId
            }).ToList();
            return Response;
        }

        public async Task<UserDetailsResponse> GetUserDetails(Guid id)
        {
            var Response = new UserDetailsResponse();
            try
            {
                var customer = await _billContext.Users.Where(c => c.UserId == id).FirstOrDefaultAsync();
                if (customer == null)
                {
                    Response.StatusCode = StatusCode.error.ToString();
                    Response.Message = "There is no Customer with this Id";
                    return Response;
                }
                Response.StatusCode = StatusCode.success.ToString();
                Response.Message = "Data Returned Successfully";
                Response.data = new PartnerOutPut()
                {
                    Id = customer.UserId,
                    Name = customer.FullName
                };
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCode.error.ToString();
                Response.Message = "Validation Error";
            }
            return Response;
        }
    }
}
