using bill_payment.Enums;

namespace bill_payment.Models.Partners
{
    public class PartnerInput
    {
        public string Name { get; set; }
        public string SPocEmail { get; set; }
        public PartnerStatus status { get; set; }
        public bool IsGedieaPayEnabled { get; set; }
        public bool IsPartnerWalletEnabled { get; set; }
        public int SessionTimeInMins { get; set; }
    }
}
