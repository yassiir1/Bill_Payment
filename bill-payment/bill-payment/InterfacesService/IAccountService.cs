using bill_payment.Models.Account;
using bill_payment.Models.Partners;

namespace bill_payment.InterfacesService
{
    public interface IAccountService
    {
        Task<LoginResponse> LoginAsync(LoginInput data);

    }
}
