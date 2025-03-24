namespace bill_payment.Models.FavouritePayment
{
    public class FavouritePaymentOutput
    {
        public int favorite_payment_id { get; set; }
        public string service_provider_code { get; set; }
        public string service_code { get; set; }
        public string user_account { get; set; }
        public string package_code { get; set; }
        public string? prereq_service_code { get; set; }
        public string payment_name { get; set; }
        public int bill_type { get; set; }
        public double last_paid_amount { get; set; }
        public bool is_bill { get; set; }
        public double current_bill_amount { get; set; }
        public string? external_txn_id { get; set; }
        public string? external_ref_number { get; set; }
    }
}
