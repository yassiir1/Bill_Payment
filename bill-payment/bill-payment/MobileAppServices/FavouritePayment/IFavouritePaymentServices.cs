using bill_payment.Models.FavouritePayment;

namespace bill_payment.MobileAppServices.FavouritePayment
{
    public interface IFavouritePaymentServices
    {
        Task<AddFavouritePaymentResponse> AddFavouritePayment(FavouritePaymentInput input);
        Task<FavouritePaymentResponse> GetFavouritePayment();
        Task<string> DeleteFavouritePayment(int id);
    }
}
