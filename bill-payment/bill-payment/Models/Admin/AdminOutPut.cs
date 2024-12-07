namespace bill_payment.Models.Admin
{
    public class AdminOutPut
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<AdminDto> data { get; set; }
 
    }
}
    