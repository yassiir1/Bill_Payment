namespace bill_payment.Models.Users
{
    public class LoginResponse
    {
        public string Message { get; set; }
        public LoginOutput data { get; set; }
      
    }
    public class LoginOutput
    {
        public string user_id { get; set; }
        public string session_id { get; set; }
        public string skey { get; set; }
        public string fullname { get; set; }
        public string birth_date { get; set; }
        public string phone { get; set; }
        public string national_id { get; set; }
        public string gendder { get; set; }
    }
}
