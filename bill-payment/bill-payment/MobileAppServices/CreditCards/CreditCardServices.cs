using bill_payment.BillDbContext;
using bill_payment.Models.CreditCards;
using bill_payment.Domains;
using Microsoft.EntityFrameworkCore;


namespace bill_payment.MobileAppServices.CreditCards
{
    public class CreditCardServices : ICreditCardsServices
    {
        private readonly Bill_PaymentContext _billContext;
        public CreditCardServices(Bill_PaymentContext billContext)
        {
           _billContext = billContext;
        }
        public async Task<AddCreditCardResponse> AddCreditCard(CreditCardInput input)
        {
            var data = new Domains.CreditCards()
            {
                cvv = input.cvv,
                holder_name = input.holder_name,
                last_4_digit = input.last_4_digit,
                month = input.month,
                token_id = input.token_id,
                type = input.type,
                year = input.year,
            };
            await _billContext.CreditCards.AddAsync(data);
            await _billContext.SaveChangesAsync();
            return new AddCreditCardResponse()
            {
                Message = "Credit Card Successfully Added!",
                data = new AddCreditCatdOutput() { card_id = data.Id},
            };
        }

        public async Task<string> DeleteCreditCard(int id)
        {
            var card = await _billContext.CreditCards.FindAsync(id);
             _billContext.CreditCards.Remove(card);
            await _billContext.SaveChangesAsync();
            return "Card Successfully Deleted!";
        }

        public async Task<CreditCardResponse> ListCreditCards()
        {
            var cards = await _billContext.CreditCards.ToListAsync();
            return new CreditCardResponse()
            {
                Message = "Cards Successfully loaded!",
                data = cards.Select(c => new CreditCardOutput()
                {
                    cvv = c.cvv,
                    holder_name = c.holder_name,
                    last_4_digit = c.last_4_digit,
                    month = c.month,
                    token_id = c.token_id,
                    type = c.type,
                    year = c.year,
                    Id = c.Id

                }).ToList()
            };
        }
    }
}
