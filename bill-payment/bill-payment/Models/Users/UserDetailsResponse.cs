using bill_payment.Models.Partners;

namespace bill_payment.Models.Users
{
    public class UserDetailsResponse
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public PartnerOutPut data { get; set; }
    }
}
