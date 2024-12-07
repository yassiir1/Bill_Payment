using System.ComponentModel.DataAnnotations;

namespace bill_payment.Domains
{
    public class Admin
    {
        [Key]
        public Guid AdminId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
