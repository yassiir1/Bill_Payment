namespace bill_payment.Models.Users
{
    public class GeideaLoginResponse
    {
        public string user_id { get; set; }
        public string user_status { get; set; }
        public string session_id { get; set; }
        public string skey { get; set; }
        public string full_name { get; set; }
        public decimal current_balance { get; set; }
        public decimal opening_balance { get; set; }
        public int reset_password_1_flag { get; set; }
        public int reset_password_2_flag { get; set; }
        public decimal maximum_balance { get; set; }
        public List<LookupVersion> lookup_version { get; set; }
        public ExtraInfo extra_info { get; set; }
        public string status_code { get; set; }
        public string status_description { get; set; }
        public string status_msg { get; set; }
    }

    public class LookupVersion
    {
        public string lookup_code { get; set; }
        public int current_version { get; set; }
    }

    public class StoreData
    {
        public string terminal_user_id { get; set; }
        public string store_account { get; set; }
        public string merchant_account { get; set; }
        public string merchant_name { get; set; }
    }

    public class UserWalletBalance
    {
        public string dedicated_wallet_code { get; set; }
        public string description { get; set; }
        public string description_ar { get; set; }
        public decimal current_balance { get; set; }
    }

    public class ExtraInfo
    {
        public StoreData store_data { get; set; }
        public List<UserWalletBalance> user_wallet_balances { get; set; }
    }
}
