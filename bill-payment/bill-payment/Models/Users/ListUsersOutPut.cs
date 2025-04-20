namespace bill_payment.Models.Users
{
    public class ListUsersOutPut
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string PartnerName { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public DateTime CreationDate { get; set; }
    }
    
}
