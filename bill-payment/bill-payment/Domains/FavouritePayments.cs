using System.ComponentModel.DataAnnotations;

namespace bill_payment.Domains
{
    public class FavouritePayments
    {
        [Key]
        public int Id { get; set; }
        public string service_provider_code { get; set; }
        public string service_code { get; set; }
        public string user_account { get; set; }
        public string package_code { get; set; }
        public int bill_type { get; set; }
        public double last_paid_amount { get; set; }
        public bool is_bill { get; set; }

    }
}
