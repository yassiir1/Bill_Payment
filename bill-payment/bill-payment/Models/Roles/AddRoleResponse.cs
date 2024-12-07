using bill_payment.Domains;

namespace bill_payment.Models.Roles
{
    public class AddRoleResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public Domains.Roles Roles { get; set; }
    }
}
