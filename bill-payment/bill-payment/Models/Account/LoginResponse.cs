namespace bill_payment.Models.Account
{
    public class LoginResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public LoginOutput data { get; set; }
    }
}
