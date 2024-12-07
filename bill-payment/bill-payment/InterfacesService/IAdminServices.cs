using bill_payment.Domains;
using bill_payment.Models;
using bill_payment.Models.Admin;

namespace bill_payment.InterfacesService
{
    public interface IAdminServices
    {
        Task<AddAdminResponse> AddAdmin(AdminInput data, string Token);
        Task<AdminOutPut> GetAllAdmins();

    }
}
