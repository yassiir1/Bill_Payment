using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bill_payment.Domains
{
    public class Partner
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        
    }
}
