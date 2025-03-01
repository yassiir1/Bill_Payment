using System.ComponentModel.DataAnnotations;

namespace bill_payment.Domains
{
    public class Setting
    {
        [Key]
        public int Id { get; set; }
        public string primary_color {  get; set; }
        public int service_columns_number { get; set; }
        public List<Banners> banners { get; set; }
    }
}
