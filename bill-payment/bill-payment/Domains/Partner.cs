using bill_payment.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bill_payment.Domains
{
    public class Partner
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string SPocEmail { get; set; }
        public DateTime CreationDate { get; set; }
        public CustomerRegisterationPolicy customerRegisterationPolicy { get; set; }
        public PartnerStatus status { get; set; }
        public bool IsGedieaPayEnabled { get; set; }
        public bool IsPartnerWalletEnabled { get; set; }
        public int SessionTimeInMins { get; set; }
        
    }
}
