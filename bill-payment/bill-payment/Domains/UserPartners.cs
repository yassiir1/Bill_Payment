using System.ComponentModel.DataAnnotations;

namespace bill_payment.Domains
{
    public class UserPartners
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PartnerId { get; set; }
        public User User { get; set; }
        public Partner Partner { get; set; }
    }
}
