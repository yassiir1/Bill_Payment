using bill_payment.Models;
using bill_payment.Models.CreditCards;

namespace bill_payment.MobileAppServices.CreditCards
{
    public interface ICreditCardsServices
    {
        Task<AddCreditCardResponse> AddCreditCard(CreditCardInput creditCardOutput);
        Task<CreditCardResponse> ListCreditCards(PaginatoinClass filter);
        Task<string> DeleteCreditCard(int id);
    }
}
