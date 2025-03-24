using System.ComponentModel.DataAnnotations;

namespace bill_payment.Domains
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();
        public Guid? PartnerId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string? GiedeaUser_id { get; set; }
        public string? session_id { get; set; }
        public string? skey { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

    }
}
