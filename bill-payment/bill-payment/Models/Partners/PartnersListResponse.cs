namespace bill_payment.Models.Partners
{
    public class PartnersListResponse 
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public List<PartnerOutPut> data { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public int totalRecords { get; set; }

    }
}
