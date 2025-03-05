using bill_payment.Models.CreditCards;

namespace bill_payment.MobileAppServices.CreditCards
{
    public interface ICreditCardsServices
    {
        Task<AddCreditCardResponse> AddCreditCard(CreditCardInput creditCardOutput);
        Task<CreditCardResponse> ListCreditCards();
        Task<string> DeleteCreditCard(int id);
    }
}
