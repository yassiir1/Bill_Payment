using bill_payment.Enums;

namespace bill_payment.Models.Partners
{
    public class PartnerFilter : PaginatoinClass
    {
        public string? name { get; set; }
        public DateTime? creationDateFrom { get; set; }
        public DateTime? creationDateTo { get; set; }
        public PartnerStatus? status { get; set; }

    }
}
