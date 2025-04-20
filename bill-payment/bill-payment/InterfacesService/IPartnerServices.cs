using bill_payment.Models.Admin;
using bill_payment.Models.Partners;

namespace bill_payment.InterfacesService
{
    public interface IPartnerServices
    {
        Task<AddPartnerResponse> AddPartner(PartnerInput data);
        Task<AddPartnerResponse> EditPartner(Guid id,PartnerInput data);
        Task<PartnersListResponse> ListPartners(PartnerFilter filter);
        Task<DeletePartnerResponse> DeletePartner(Guid id);
        Task<PartnerDetailsResponse> GetPartnerDetails(Guid id);

    }
}
