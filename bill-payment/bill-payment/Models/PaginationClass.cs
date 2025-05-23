namespace bill_payment.Models
{
    public class PaginationClass
    {
        public int current_page { get; set; }
        public int total_page { get; set; }
        public int per_page { get; set; }
        public int total_records { get; set; }

    }
}
