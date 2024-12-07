namespace bill_payment.Models.Users
{
    public class UserInput
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public Guid? PartnerId { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
