using System.ComponentModel.DataAnnotations;

namespace bill_payment.Domains
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public Guid? PartnerId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

    }
}
