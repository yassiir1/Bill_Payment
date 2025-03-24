using bill_payment.MobileAppServices.CreditCards;
using bill_payment.Models.CreditCards;
using bill_payment.Models.FavouritePayment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bill_payment.Controllers
{
    [Route("api/credit-card")]
    [ApiController]
    public class CreditCardsController : ControllerBase
    {
        private readonly ICreditCardsServices _creditCardsServices;
        public CreditCardsController(ICreditCardsServices creditCardsServices)
        {
            _creditCardsServices = creditCardsServices;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddCard([FromBody] CreditCardInput data)
        {
            var Response = await _creditCardsServices.AddCreditCard(data);
            return Ok(Response);
        }
        [HttpDelete("remove/{CARD_ID}")]
        public async Task<IActionResult> DeleteFavourite( int CARD_ID)
        {
            var Response = await _creditCardsServices.DeleteCreditCard(CARD_ID);
            return Ok(new { Message = Response });
        }
        [HttpGet]
        public async Task<IActionResult> ListCreditCards()
        {
            var Response = await _creditCardsServices.ListCreditCards();
            return Ok(Response);
        }
    }
}
