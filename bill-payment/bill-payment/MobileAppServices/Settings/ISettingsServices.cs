using bill_payment.Models.Settings;

namespace bill_payment.MobileAppServices.Settings
{
    public interface ISettingsServices
    {
        Task<SettingsResponse> ListSettings();
        Task<BannersResponse> ListBanners();
        Task<DeleteBannerResponse> DeleteBanner(int id);
        Task<AddBannerResponse> EditBanner(int id, AddBanner data);
        Task<AddBannerResponse> AddBanner(AddBanner data);
        Task<SettingsResponse> EditSettings(EditSettings data);
    }
}
