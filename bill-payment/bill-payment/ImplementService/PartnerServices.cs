using bill_payment.BillDbContext;
using bill_payment.Domains;
using bill_payment.Enums;
using bill_payment.InterfacesService;
using bill_payment.Models.Partners;
using Microsoft.EntityFrameworkCore;

namespace bill_payment.ImplementService
{
    public class PartnerServices : IPartnerServices
    {
        public readonly Bill_PaymentContext _billContext;
        public PartnerServices(Bill_PaymentContext billContext)
        {
            this._billContext = billContext;
        }
        public async Task<AddPartnerResponse> AddPartner(PartnerInput data)
        {
            var Response = new AddPartnerResponse();
            try
            {
                var Partner = new Partner
                {
                    Name = data.Name,
                    CreationDate = DateTime.Now.ToUniversalTime(),
                    IsGedieaPayEnabled = data.IsGedieaPayEnabled,
                    SPocEmail = data.SPocEmail,
                    IsPartnerWalletEnabled = data.IsPartnerWalletEnabled,
                    SessionTimeInMins = data.SessionTimeInMins,
                    status = data.status
                    
                };
                await _billContext.Partner.AddAsync(Partner);
                await _billContext.SaveChangesAsync();
                Response.Message = "Partner Added Successfully";
                Response.StatusCode = 200;
                Response.data = new PartnerOutPut
                {
                    Id = Partner.Id,
                    Name = Partner.Name,
                };
            }
            catch (Exception ex) {
                Response.Message = "Error While Adding Partner";
                Response.StatusCode = 400;
                Response.data = new PartnerOutPut();
            }
            return Response;
        }

        public async Task<DeletePartnerResponse> DeletePartner(Guid id)
        {
            var Response = new DeletePartnerResponse();
            var Partner = await _billContext.Partner.Where(c=> c.Id == id).FirstOrDefaultAsync();
            if (Partner == null) {
                Response.StatusCode = StatusCode.error.ToString();
                Response.Message = "No Partner Founded With This Id";
                return Response;
            }
            _billContext.Partner.Remove(Partner);
            await _billContext.SaveChangesAsync();
            Response.Message = "Partner Deleted Successfully";
            Response.StatusCode = StatusCode.success.ToString();  
            return Response;
        }

        public async Task<AddPartnerResponse> EditPartner(Guid id ,PartnerInput data)
        {
            var Response = new AddPartnerResponse();
            try
            {
                var Partner = await _billContext.Partner.Where(c => c.Id == id).FirstOrDefaultAsync();
                if (Partner == null)
                {
                    Response.StatusCode = 400;
                    Response.data = new PartnerOutPut();
                    Response.Message = "No Partner With This Id Founded";
                    return Response;
                }
                Partner.Name = data.Name;
                Partner.status = data.status;
                Partner.IsPartnerWalletEnabled = data.IsPartnerWalletEnabled;
                Partner.IsGedieaPayEnabled = data.IsGedieaPayEnabled; 
                Partner.SessionTimeInMins = data.SessionTimeInMins;
                Partner.SPocEmail = data.SPocEmail;

                await _billContext.SaveChangesAsync();
                Response.StatusCode = 200;
                Response.data = new PartnerOutPut
                {
                    Id = Partner.Id,
                    Name = Partner.Name,
                };
                Response.Message = "Partner Created Successfully";
                return Response;

            }
            catch (Exception ex) {
                Response.StatusCode = 400;
                Response.data = new PartnerOutPut();
                Response.Message = "Error While Creating Partner";
                return Response;
            }
        }

        public async Task<PartnerDetailsResponse> GetPartnerDetails(Guid id)
        {
            var Response = new PartnerDetailsResponse();
            try
            {
                var partner = await _billContext.Partner.Where(c => c.Id == id).FirstOrDefaultAsync();
                if(partner == null)
                {
                    Response.StatusCode = StatusCode.error.ToString();
                    Response.Message = "No Partner With This Id";
                    return Response;
                }
                var data = new PartnerOutPut()
                {
                    Id = partner.Id,
                    Name = partner.Name,
                    CreationDate = partner.CreationDate,
                    IsGedieaPayEnabled = partner.IsGedieaPayEnabled,
                    IsPartnerWalletEnabled = partner.IsPartnerWalletEnabled,
                    SessionTimeInMins = partner.SessionTimeInMins,
                    status = partner.status.ToString(),
                    SPocEmail = partner.SPocEmail,
                    customerRegisterationPolicy = partner.customerRegisterationPolicy.ToString()
                };
                Response.StatusCode = StatusCode.success.ToString();
                Response.Message = "Data Returned Successfully";
                Response.data = data;
            }
            catch(Exception ex)
            {
                Response.StatusCode = StatusCode.error.ToString();
                Response.Message = "Validation Error";
            }
            return Response;
        }

        public async Task<PartnersListResponse> ListPartners()
        {
            var Response = new PartnersListResponse();
            var Partners =  _billContext.Partner.AsQueryable();
            Response.StatusCode = StatusCode.success.ToString();
            Response.Message = "Data Returned Successfully";
            Response.data = await Partners.Select(c=> new PartnerOutPut
            {
                Id=c.Id,
                Name=c.Name,
                CreationDate = c.CreationDate,
                IsGedieaPayEnabled = c.IsGedieaPayEnabled,
                IsPartnerWalletEnabled = c.IsPartnerWalletEnabled,
                SessionTimeInMins = c.SessionTimeInMins,
                status = c.status.ToString(),
                SPocEmail = c.SPocEmail,
                customerRegisterationPolicy = c.customerRegisterationPolicy.ToString()
            }).ToListAsync();
            return Response;
        }
    }
}
