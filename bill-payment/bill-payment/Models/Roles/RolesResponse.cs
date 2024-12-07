using bill_payment.Domains;

namespace bill_payment.Models.Roles
{
    public class RolesResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<Domains.Roles> Roles { get; set; }
    }
}
