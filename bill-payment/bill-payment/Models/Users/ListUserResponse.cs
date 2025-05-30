﻿using bill_payment.Domains;
using bill_payment.Models.Partners;

namespace bill_payment.Models.Users
{
    public class ListUserResponse 
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public List<ListUsersOutPut> data { get; set; }
        public PaginationClass pagination { get; set; }

    }
}
