using bill_payment.Enums;

namespace bill_payment.Models.Partners
{
    public class PartnerOutPut
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string status { get; set; }
        public string customerRegisterationPolicy { get; set; }
        public string SPocEmail { get; set; }
        public bool IsGedieaPayEnabled { get; set; }
        public bool IsPartnerWalletEnabled { get; set; }
        public int SessionTimeInMins { get; set; }
    }
}
