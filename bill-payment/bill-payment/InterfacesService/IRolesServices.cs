using bill_payment.Domains;
using bill_payment.Models.Roles;

namespace bill_payment.InterfacesService
{
    public interface IRolesServices
    {
        Task<AddRoleResponse> AddNewRole(RolesInput model,string Token);
        Task<RolesResponse> GetAllRoles();
        Task<DeleteRoleResponse> SoftDeleteRole(Guid Id);
    }
}
