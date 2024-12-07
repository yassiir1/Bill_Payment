namespace bill_payment.Models.Users
{
    public class AddUserResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public Domains.User data { get; set; }
    }
}
