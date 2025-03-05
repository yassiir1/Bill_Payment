
namespace bill_payment.Models.CreditCards
{
    public class CreditCardResponse
    {
        public string Message { get; set; }
        public List<CreditCardOutput> data { get; set; }
    }
}
