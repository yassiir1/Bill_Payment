using bill_payment.Domains;

namespace bill_payment.Models.Settings
{
    public class SettingsOutPut
    {

        public string primary_color {  get; set; }
        public int service_columns_number {  get; set; }
        public List<BannersOutPut> Banners {  get; set; } 

    }
}
