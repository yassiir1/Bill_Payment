namespace bill_payment.Models.Partners
{
    public class PartnersListResponse 
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public List<PartnerOutPut> data { get; set; }
        public PaginationClass pagination { get; set; }


    }
}
