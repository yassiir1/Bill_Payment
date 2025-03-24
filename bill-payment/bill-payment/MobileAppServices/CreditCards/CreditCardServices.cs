using bill_payment.BillDbContext;
using bill_payment.Models.CreditCards;
using bill_payment.Domains;
using Microsoft.EntityFrameworkCore;
using bill_payment.Models.Users;
using bill_payment.Models.FavouritePayment;


namespace bill_payment.MobileAppServices.CreditCards
{
    public class CreditCardServices : ICreditCardsServices
    {
        private readonly Bill_PaymentContext _billContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreditCardServices(Bill_PaymentContext billContext, IHttpContextAccessor httpContextAccessor)
        {
            _billContext = billContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<AddCreditCardResponse> AddCreditCard(CreditCardInput input)
        {
            var exist = await _billContext.CreditCards.AnyAsync(c => c.token_id == input.token_id);
            if (exist)
                return new AddCreditCardResponse()
                {
                    Message = "Your credit card is already exist!",
                    data = new AddCreditCatdOutput()
                };
            var UserInfo = GetUserInfos();
            if (UserInfo == null || UserInfo.UserId == null /*||  UserInfo.SessionId == null || UserInfo.Skey == null*/)
                throw new Exception("There is No User To Add Credit Card, Make sure Of Your 3 Credentials");
            var user = await _billContext.Users.Where(c => c.GiedeaUser_id == UserInfo.UserId).FirstOrDefaultAsync();
            //if (UserInfo.SessionId != user.session_id || UserInfo.Skey != user.skey)
            //    throw new Exception("Login TimeOut, Please ReLogin then try again!");
            var data = new Domains.CreditCards()
            {
                cvv = input.cvv,
                holder_name = input.holder_name,
                last_4_digit = input.last_4_digit,
                month = input.month,
                token_id = input.token_id,
                type = input.type,
                year = input.year,
                UserId = user.UserId
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
            var userInfo = GetUserInfos();
            if (userInfo == null || userInfo.UserId == null /*|| userInfo.SessionId == null || userInfo.Skey == null*/)
                throw new Exception("There is No User Id To Login With");
            var user = await _billContext.Users.Where(c => c.GiedeaUser_id == userInfo.UserId).FirstOrDefaultAsync();
            //if (userInfo.SessionId != user.session_id || userInfo.Skey != user.skey)
            //    throw new Exception("Login TimeOut, Please ReLogin then try again!");
            var card = await _billContext.CreditCards.FindAsync(id);
             _billContext.CreditCards.Remove(card);
            await _billContext.SaveChangesAsync();
            return "Card Successfully Deleted!";
        }

        public async Task<CreditCardResponse> ListCreditCards()
        {
            var UserInfo = GetUserInfos();
            if (UserInfo == null || UserInfo.UserId == null /*|| UserInfo.SessionId == null || UserInfo.Skey == null*/)
                throw new Exception("There is No User To Add Credit Card, Make sure Of Your Credentials");
            var user = await _billContext.Users.Where(c => c.GiedeaUser_id == UserInfo.UserId).FirstOrDefaultAsync();
            //if (UserInfo.SessionId != user.session_id || UserInfo.Skey != user.skey)
            //    throw new Exception("Login TimeOut, Please ReLogin then try again!");
            var cards = await _billContext.CreditCards.Where(c=> c.UserId == user.UserId).ToListAsync();
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
        private UserInfos GetUserInfos()
        {
            var userSession = _httpContextAccessor.HttpContext?.Items["UserSession"] as UserInfos;

            if (userSession != null)
            {
                var userId = userSession.UserId;
                var sessionId = userSession.SessionId;
                var skey = userSession.Skey;
            }
            return userSession;
        }
    }
}
