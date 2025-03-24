namespace bill_payment.Models.FavouritePayment
{
    public class FavouritePaymentInput
    {
        public string service_provider_code { get; set; }
        public string service_code { get; set; }
        public string user_account { get; set; }
        public string? payment_name { get; set; }
        public string? prereq_service_code { get; set; }
        public string package_code { get; set; }
        public int bill_type { get; set; }
        public double last_paid_amount { get; set; }
        public bool is_bill { get; set; }

    }
}
