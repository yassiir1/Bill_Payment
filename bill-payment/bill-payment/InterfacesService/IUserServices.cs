using bill_payment.Models.Partners;
using bill_payment.Models.Roles;
using bill_payment.Models.Users;

namespace bill_payment.InterfacesService
{
    public interface IUserServices
    {
        Task<AddUserResponse> AddUser(UserInput model);
        Task<AddUserResponse> EditUser(Guid id, UserInput model);
        Task<ListUserResponse> GetAllAsync();
        Task<ListUserResponse> GetUserByPartnerId(Guid id);
        Task<UserDetailsResponse> GetUserDetails(Guid id);
        Task<DeleteUserResponse> DeleteUser(Guid id);

    }
}
