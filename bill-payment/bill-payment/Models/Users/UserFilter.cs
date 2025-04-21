using bill_payment.Enums;

namespace bill_payment.Models.Users
{
    public class UserFilter : PaginatoinClass
    {
        public string? name { get; set; }
        public List<Guid?> partnersIds { get; set; } = new();
        public int? minAge { get; set; }
        public int? maxAge { get; set; }
        public DateTime? creationDateFrom { get; set; }
        public DateTime? creationDateTo { get; set; }
        public string? gender { get; set; }
        public List<CustomerStatus?> statuses { get; set; } = new();
       
    }
}
