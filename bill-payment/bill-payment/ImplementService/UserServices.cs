using bill_payment.BillDbContext;
using bill_payment.Domains;
using bill_payment.Enums;
using bill_payment.InterfacesService;
using bill_payment.Models.Partners;
using bill_payment.Models.Users;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.ComponentModel;

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
                    Email = model.Email,
                    PartnerId = model.PartnerId,
                };

                await _billContext.Users.AddAsync(data);
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

        public async Task<byte[]> ExportUsers(List<ListUsersOutPut> data)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Transfers");

            worksheet.Cells[1, 1].Value = "Partner";
            worksheet.Cells[1, 2].Value = "Name";
            worksheet.Cells[1, 3].Value = "Email";
            worksheet.Cells[1, 4].Value = "Phone";
            worksheet.Cells[1, 5].Value = "Creation Date";
            worksheet.Cells[1, 6].Value = "Status";
            using (var range = worksheet.Cells[1, 1, 1, 6])
            {
                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                range.Style.Font.Bold = true;
                range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            for (int i = 0; i < data.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = data[i].PartnerName;

                worksheet.Cells[i + 2, 2].Value = data[i].FullName;
                worksheet.Cells[i + 2, 3].Value = data[i].Email;
                worksheet.Cells[i + 2, 4].Value = data[i].PhoneNumber;
                worksheet.Cells[i + 2, 5].Value = data[i].CreationDate.ToString("yyyy-MM-dd");
                worksheet.Cells[i + 2, 6].Value = data[i].Status;
                using (var rowRange = worksheet.Cells[i + 2, 1, i + 2, 6])
                {
                    rowRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    rowRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Wheat);

                    rowRange.Style.Font.Bold = true;
                    rowRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                }
            }

            worksheet.Cells.AutoFitColumns();

            return await Task.FromResult(package.GetAsByteArray());
        }

        public async Task<ListUserResponse> GetAllAsync(UserFilter filter)
        {
            var Response = new ListUserResponse();
            var data =  _billContext.Users.Include(c=> c.Partner).Include(c=> c.FavouritePayments).AsQueryable();
            if (filter.statuses?.Any() == true)
                data = data.Where(c => filter.statuses.Contains(c.status));

            if (filter.partnersIds?.Any() == true)
                data = data.Where(c => filter.partnersIds.Contains(c.PartnerId));

            if (!string.IsNullOrEmpty(filter.gender))
                data = data.Where(c => c.Gender == filter.gender);

            if(filter.creationDate != null)
                data = data.Where(c=> c.CreationDate == filter.creationDate.Value.ToUniversalTime());

            if (filter.minAge.HasValue)
                data = data.Where(c => c.Age >= filter.minAge.Value);

            if (filter.maxAge.HasValue)
                data = data.Where(c => c.Age <= filter.maxAge.Value);

            if (!string.IsNullOrEmpty(filter.name))
                data = data.Where(c => EF.Functions.Like(c.FullName.ToLower(), $"{filter.name.ToLower()}%"));

            var page = filter.page > 0 ? filter.page : 1;
            var pageSize = filter.pageSize > 0 ? filter.pageSize : 10;
            data = data.Skip((page - 1) * pageSize).Take(pageSize);

            Response.StatusCode = StatusCode.success.ToString();
            Response.Message = "Data Returned successfully";
            Response.page = page;
            Response.pageSize = pageSize;
            Response.data = await data.Select(c => new ListUsersOutPut()
            {
                NationalId = c.NationalId,
                UserId = c.UserId,
                CreationDate = c.CreationDate,
                DateOfBirth = c.DateOfBirth,
                FullName = c.FullName,
                Gender = c.Gender,
                PhoneNumber = c.PhoneNumber,
                Email = c.Email,
                Age = c.Age,
                PartnerName = c.Partner.Name,
                Status = c.status.ToString(),
            }).ToListAsync();
            return Response;

        }


        public async Task<UserDetailsResponse> GetUserDetails(Guid id)
        {
            var Response = new UserDetailsResponse();
            try
            {
                var customer = await _billContext.Users.Include(c=> c.FavouritePayments).Where(c => c.UserId == id).Include(c => c.FavouritePayments).FirstOrDefaultAsync();
                if (customer == null)
                {
                    Response.StatusCode = StatusCode.error.ToString();
                    Response.Message = "There is no Customer with this Id";
                    return Response;
                }
                Response.StatusCode = StatusCode.success.ToString();
                Response.Message = "Data Returned Successfully";
                Response.data = new UserDetailsOutPut()
                {
                    Name = customer.FullName,
                    Age = customer.Age,
                    CreationDate = customer.CreationDate.ToUniversalTime().ToString("yyyy-MM-dd"),
                    Email = customer.Email,
                    Gender = customer.Gender,
                    Phone = customer.PhoneNumber,
                    QuickPays = customer.FavouritePayments.Select(c => new UserQuickPayOutPut()
                    {
                        ServiceAmount = c.last_paid_amount,
                        ServiceName = c.PaymentName
                    }).ToList()
                }; 
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCode.error.ToString();
                Response.Message = "Validation Error";
            }
            return Response;
        }


        //public async Task<ListUserResponse> GetUserByPartnerId(Guid id)
        //{
        //    var Response = new ListUserResponse();

        //    var data = await _billContext.Users.Where(c => c.PartnerId == id).ToListAsync();
        //    if (data.Count == 0)
        //    {
        //        Response.StatusCode = StatusCode.error.ToString();
        //        Response.Message = "There are no Customers with this partner";
        //        return Response;
        //    }
        //    Response.StatusCode = StatusCode.success.ToString();
        //    Response.Message = "Data Returned Successfully";
        //    Response.data.Select(c => new ListUsersOutPut()
        //    {
        //        CreationDate = c.CreationDate,
        //        DateOfBirth = c.DateOfBirth,
        //        FullName = c.FullName,
        //        Gender = c.Gender,
        //        NationalId = c.NationalId,
        //        PhoneNumber = c.PhoneNumber,
        //        UserId = c.UserId
        //    }).ToList();
        //    return Response;
        //}

    }
}
