namespace bill_payment.Models.Admin
{
    public class AdminDetailOutPut
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public AdminDto data { get; set; }
    }
}
