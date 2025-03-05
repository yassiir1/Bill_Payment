using bill_payment.ImplementService;
using bill_payment.InterfacesService;
using bill_payment.MobileAppServices.CreditCards;
using bill_payment.MobileAppServices.FavouritePayment;
using bill_payment.MobileAppServices.Settings;

namespace bill_payment.ServicesExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRolesServices, RolesServices>();
            services.AddScoped<IPartnerServices, PartnerServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IAdminServices, AdminServices>();
            services.AddScoped<IAccountService, AccountServices>();
            services.AddScoped<ISettingsServices, SettingsServices>();
            services.AddScoped<IFavouritePaymentServices, FavouritePaymentServices>();
            services.AddScoped<ICreditCardsServices, CreditCardServices>();

            return services;
        }
    }
}
