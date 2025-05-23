using bill_payment.Domains;
using bill_payment.Models;
using bill_payment.Models.Admin;

namespace bill_payment.InterfacesService
{
    public interface IAdminServices
    {
        Task<AddAdminResponse> AddAdmin(AdminInput data, string Token);
        Task<AddAdminResponse> EditAdmin(Guid adminId, AdminEditInput data, string token);
        Task<AddAdminResponse> DeleteAdmin(Guid adminId, string token);
        Task<AdminDetailOutPut?> GetAdminDetails(Guid adminId);
        Task<AdminOutPut> GetAllAdmins();

    }
}
