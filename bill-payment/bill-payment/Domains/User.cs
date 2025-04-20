using bill_payment.Enums;
using System.ComponentModel.DataAnnotations;

namespace bill_payment.Domains
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();
        public Guid PartnerId { get; set; }
        public Partner Partner { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; private set; } 
        public string Gender { get; set; }
        public string? GiedeaUser_id { get; set; }
        public string? session_id { get; set; }
        public string? skey { get; set; }
        public CustomerStatus status { get; set; }
        public List<FavouritePayments> FavouritePayments { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public User()
        {
            Age = GetAge(); 
        }
        private int GetAge()
        {
            return DateTime.Now.Year - DateOfBirth.Year;
        }
    }

}
