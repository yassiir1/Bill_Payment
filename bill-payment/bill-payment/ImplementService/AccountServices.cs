using bill_payment.InterfacesService;
using bill_payment.Models.Account;

namespace bill_payment.ImplementService
{
    public class AccountServices : IAccountService
    {
        private static readonly HttpClient _client = new HttpClient();
        private string BaseUrl = "http://localhost:8080";
        //private string BaseUrl = "http://127.0.0.1:8280";

        public async Task<LoginResponse> LoginAsync(LoginInput data)
        {
            var Response = new LoginResponse();
            string tokenEndpoint = $"{BaseUrl}/auth/realms/bill-payment-sdk/protocol/openid-connect/token";
            var formContent = new FormUrlEncodedContent(new[]
       {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("client_id", "bill-payment-sdk-service"),
            new KeyValuePair<string, string>("client_secret", "UGz04k1gJ3xRLY7Zts7DYLERTTvP21Di"), // Optional if the client requires it
            //new KeyValuePair<string, string>("client_secret", "91cipeTikJmcok426J8ue5QkkICJHHLp"), // Optional if the client requires it
            new KeyValuePair<string, string>("username", data.UserName),
            new KeyValuePair<string, string>("password", data.Password)
        });
            HttpResponseMessage response = await _client.PostAsync(tokenEndpoint, formContent);
            if (response.IsSuccessStatusCode)
            {
                // Parse the response if successful
                var tokenResponse = await response.Content.ReadFromJsonAsync<LoginOutput>();
                Response.Message = "Logged in Successfully";
                Response.StatusCode = 200;
                Response.data = tokenResponse;
            }
            else
            {
                var tokenResponse = await response.Content.ReadFromJsonAsync<LoginOutput>();
                Response.Message = $"Failed While Logging in - {response.ReasonPhrase}";
                Response.StatusCode = 400;
                Response.data = new LoginOutput();
            }
            return Response;

        }
    }
}
