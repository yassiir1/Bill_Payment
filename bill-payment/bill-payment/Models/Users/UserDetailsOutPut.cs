namespace bill_payment.Models.Users
{
    public class UserDetailsOutPut
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CreationDate { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public List<UserQuickPayOutPut> QuickPays { get; set; }
    }
}
