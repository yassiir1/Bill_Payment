using bill_payment.BillDbContext;
using bill_payment.Domains;
using bill_payment.Models.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace bill_payment.MobileAppServices.Settings
{
    public class SettingsServices : ISettingsServices
    {
        private readonly Bill_PaymentContext _billContext;

        public SettingsServices(Bill_PaymentContext billContext)
        {
            _billContext = billContext;
        }

        public async Task<AddBannerResponse> AddBanner(AddBanner data)
        {
            var settings = await _billContext.Setting.FirstOrDefaultAsync();
            var input = new Banners()
            {
                image = data.image,
                path = data.path,
                SettingId = settings.Id
            };
            await _billContext.Banners.AddAsync(input);
            await _billContext.SaveChangesAsync();
            return new AddBannerResponse()
            {
                Message = "Banners Added Successfully",
                data = new BannersOutPut()
                {
                    Id = input.Id,
                    image = input.image,
                    path = input.path,
                }
            };
        }

        public async Task<SettingsResponse> EditSettings(EditSettings input)
        {
            var data = await _billContext.Setting.Include(c => c.banners).FirstOrDefaultAsync();
            data.primary_color = input.primary_color;
            data.service_columns_number = input.service_columns_number;
            await _billContext.SaveChangesAsync();
            return new SettingsResponse()
            {
                data = new SettingsOutPut()
                {
                    Banners = data.banners.Select(c => new BannersOutPut()
                    {
                        Id = c.Id,
                        image = c.image,
                        path = c.path,
                    }).ToList(),
                    primary_color = data.primary_color,
                    service_columns_number = data.service_columns_number
                },
                Message = "Settings Updated Successfully"
            };
        }

        public async Task<BannersResponse> ListBanners()
        {
            var data = await _billContext.Banners.ToListAsync();
            return new BannersResponse()
            {
                Message = "Banners Loaded Successfully",
                data = data.Select(c => new BannersOutPut()
                {
                    Id = c.Id,
                    image = c.image,
                    path = c.path,
                }).ToList(),
            };
        }

        public async Task<SettingsResponse> ListSettings()
        {
            var data = await _billContext.Setting.Include(c => c.banners).FirstOrDefaultAsync();
            return new SettingsResponse()
            {
                data = new SettingsOutPut()
                {
                    Banners = data.banners.Select(c => new BannersOutPut()
                    {
                        Id = c.Id,
                        image = c.image,
                        path = c.path,
                    }).ToList(),
                    primary_color = data.primary_color,
                    service_columns_number = data.service_columns_number
                },
                Message = "Settings Loaded Successfully"
            };
        }
    }
}
