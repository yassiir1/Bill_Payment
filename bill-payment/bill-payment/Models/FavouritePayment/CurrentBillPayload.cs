namespace bill_payment.Models.FavouritePayment
{
    public class CurrentBillPayload
    {
        public string Payment_Network_Code { get; set; } = "CACPN";
        public string Skey { get; set; }
        public string User_Id { get; set; }
        public string Session_Id { get; set; }
        public string User_A_Pin { get; set; } = "";
        public string Service_Provider_Code { get; set; } = "Tameely_code";
        public string Service_Code { get; set; } = "TAMWEELY_DRCT_BE";
        public string User_B_Account_Id { get; set; }
        public string External_Txn_Id { get; set; } = GenerateRandomNumber(14); 
        public string External_Ref_Number { get; set; } = GenerateRandomNumber(14); 
        public string Package_Code { get; set; } = "";
        public string Package_Evd_Selector { get; set; } = "";
        public int Number_Of_Packages { get; set; } = 1;
        public int Transaction_Value { get; set; } = 1;
        public Dictionary<string, object> In_Parameters { get; set; } = new();
        private static string GenerateRandomNumber(int length)
        {
            Random random = new();
            return string.Concat(Enumerable.Range(0, length).Select(_ => random.Next(0, 10).ToString()));
        }
    }
}
