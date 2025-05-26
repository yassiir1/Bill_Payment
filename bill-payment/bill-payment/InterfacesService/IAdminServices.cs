using bill_payment.Domains;
using bill_payment.Models;
using bill_payment.Models.Admin;
using bill_payment.Models.Users;

namespace bill_payment.InterfacesService
{
    public interface IAdminServices
    {
        Task<AddAdminResponse> AddAdmin(AdminInput data, string Token);
        Task<AddAdminResponse> EditAdmin(Guid adminId, AdminEditInput data, string token);
        Task<DeleteUserResponse> DeleteAdmin(Guid adminId, string token);
        Task<AdminDetailOutPut?> GetAdminDetails(Guid adminId);
        Task<AdminOutPut> GetAllAdmins(PaginatoinClass filter);

    }
}
