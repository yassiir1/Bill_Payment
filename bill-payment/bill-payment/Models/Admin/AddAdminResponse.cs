using bill_payment.Domains;

namespace bill_payment.Models.Admin
{
    public class AddAdminResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public AdminDto? data { get; set; }
    }
}
