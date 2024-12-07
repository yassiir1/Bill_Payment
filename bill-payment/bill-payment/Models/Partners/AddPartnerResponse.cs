namespace bill_payment.Models.Partners
{
    public class AddPartnerResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public PartnerOutPut data { get; set; }
    }
}
