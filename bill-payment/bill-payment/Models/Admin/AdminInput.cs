﻿namespace bill_payment.Models.Admin
{
    public class AdminInput
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Guid? RoleId { get; set; }
    }
}
