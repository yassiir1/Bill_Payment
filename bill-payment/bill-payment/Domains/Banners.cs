using System.ComponentModel.DataAnnotations;

namespace bill_payment.Domains
{
    public class Banners
    {
        [Key]
        public int Id { get; set; }
        public string image { get; set; }
        public string? path { get; set; }
        public int SettingId { get; set; }
        public Setting? setting { get; set; }

    }
}
