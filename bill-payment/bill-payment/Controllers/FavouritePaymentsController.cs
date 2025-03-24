using bill_payment.MobileAppServices.FavouritePayment;
using bill_payment.Models.Admin;
using bill_payment.Models.FavouritePayment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bill_payment.Controllers
{
    [Route("api/favorite-payment")]
    [ApiController]
    public class FavouritePaymentsController : ControllerBase
    {
        private readonly IFavouritePaymentServices _favouritePaymentServices;
        public FavouritePaymentsController(IFavouritePaymentServices favouritePaymentServices)
        {
            _favouritePaymentServices = favouritePaymentServices;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddFavourite([FromBody] FavouritePaymentInput data)
        {
            var Response = await _favouritePaymentServices.AddFavouritePayment(data);
            return Ok(Response);
        }
        [HttpDelete("remove/{FAVORITE_PAYMENT_ID}")]
        public async Task<IActionResult> DeleteFavourite( int FAVORITE_PAYMENT_ID)
        {
            var Response = await _favouritePaymentServices.DeleteFavouritePayment(FAVORITE_PAYMENT_ID);
            return Ok(new {Message = Response});
        }
        [HttpGet]
        public async Task<IActionResult> GetFavourites()
        {
            var Response = await _favouritePaymentServices.GetFavouritePayment();
            return Ok(Response);
        }
    }
}
