using Azure.Core;
using bill_payment.BillDbContext;
using bill_payment.InterfacesService;
using bill_payment.Models.Users;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;

namespace bill_payment.ImplementService
{
    public class UserLogin : IUserLogin
    {

        private readonly HttpClient _httpClient;

        private readonly Bill_PaymentContext _billContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserLogin(Bill_PaymentContext billContext, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _billContext = billContext;
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<LoginResponse> LoginAsync()
        {
            var userInfo = GetUserInfos();
            if (userInfo == null || userInfo.UserId == null)
                throw new Exception("There is No User Id To Login With");
            var user = await _billContext.Users.Where(c => c.GiedeaUser_id == userInfo.UserId).FirstOrDefaultAsync();
            var LoginPayload = new UserLoginModel();
            string url = "https://test.geidea.net:9090/mfsuserfunctions/ChannelLoginWLV";
            var jsonContent = JsonSerializer.Serialize(LoginPayload);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Login failed: {response.StatusCode}");
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonSerializer.Deserialize<GeideaLoginResponse>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            user.skey = loginResponse.skey;
            user.session_id = loginResponse.session_id;
            await _billContext.SaveChangesAsync();
            return new LoginResponse()
            {
                Message = "success",
                data = new LoginOutput()
                {
                    user_id = userInfo.UserId,
                    session_id = user.session_id,
                    birth_date = user.DateOfBirth.ToString("yyyy-MM-dd"),
                    fullname = user.FullName,
                    gendder = user.Gender,
                    national_id = user.NationalId,
                    phone = user.PhoneNumber,
                    skey = user.skey
                }
                
            };
        }
        private UserInfos GetUserInfos()
        {
            var userSession = _httpContextAccessor.HttpContext?.Items["UserSession"] as UserInfos;

            if (userSession != null)
            {
                var userId = userSession.UserId;
                var sessionId = userSession.SessionId;
                var skey = userSession.Skey;
            }
            return userSession;
        }
    }
}
