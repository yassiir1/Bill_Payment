using System.ComponentModel.DataAnnotations;

namespace bill_payment.Domains
{
    public class Roles
    {
        [Key]
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string? RoleDescription { get; set; }
        
    }
}
