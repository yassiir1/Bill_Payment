using bill_payment.Models.Users;

namespace bill_payment.InterfacesService
{
    public interface IUserLogin
    {
        Task<LoginResponse> LoginAsync();

    }
}
