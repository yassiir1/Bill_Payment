﻿namespace bill_payment.Models.CreditCards
{
    public class CreditCardOutput
    {
        public int Id { get; set; }
        public string token_id { get; set; }
        public int last_4_digit { get; set; }
        public string type { get; set; }
        public string holder_name { get; set; }
        public int cvv { get; set; }
        public int month { get; set; }
        public int year { get; set; }
    }
}
