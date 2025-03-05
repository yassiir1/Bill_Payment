using bill_payment.BillDbContext;
using bill_payment.Domains;
using bill_payment.Models.FavouritePayment;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace bill_payment.MobileAppServices.FavouritePayment
{
    public class FavouritePaymentServices : IFavouritePaymentServices
    {
        private readonly HttpClient _httpClient;

        private readonly Bill_PaymentContext _billContext;
        public FavouritePaymentServices(Bill_PaymentContext billContext, HttpClient httpClient)
        {
            _billContext = billContext;
            _httpClient = httpClient;
        }
        public async Task<AddFavouritePaymentResponse> AddFavouritePayment(FavouritePaymentInput input)
        {
            var data = new FavouritePayments()
            {
                bill_type = input.bill_type,
                is_bill = input.is_bill,
                last_paid_amount = input.last_paid_amount,
                package_code = input.package_code,
                service_code = input.service_code,
                service_provider_code = input.service_provider_code,
                user_account = input.user_account,
            };
            await _billContext.FavouritePayments.AddAsync(data);
            await _billContext.SaveChangesAsync();
            
            return new AddFavouritePaymentResponse()
            {
                Message = "Favourite Payment successfully added!",
                data = new AddFavouritePaymentOutput() {favorite_payment_id = data.Id }
            };
        }

        public async Task<string> DeleteFavouritePayment(int id)
        {
            var data = await _billContext.FavouritePayments.FindAsync(id);
            _billContext.FavouritePayments.Remove(data);
            await _billContext.SaveChangesAsync();
            return "Favourite Payment Successfully Deleted!";
        }

        public async Task<FavouritePaymentResponse> GetFavouritePayment()
        {
            var data = await _billContext.FavouritePayments.ToListAsync();
            var tasks = data.Select(async c => await CheckForGetCurrentBillAsync(new FavouritePaymentOutput()
            {
                favorite_payment_id = c.Id,
                user_account = c.user_account,
                bill_type = c.bill_type,
                is_bill = c.is_bill,
                last_paid_amount = c.last_paid_amount,
                package_code = c.package_code,
                service_code = c.service_code,
                service_provider_code = c.service_provider_code,
            })).ToList();
            var modifiedData = (await Task.WhenAll(tasks)).ToList();
            return new FavouritePaymentResponse()
            {
                Message = data.Count() == 0 ? "You Don't have favourite payments yet!" : "Favourite Payments Successfully Loaded!",
                data = modifiedData
            };
        }
        private async Task<FavouritePaymentOutput> CheckForGetCurrentBillAsync(FavouritePaymentOutput data)
        {
            if(data.is_bill)
            {
                var request = new CurrentBillPayload()
                {
                    User_B_Account_Id = data.user_account
                };
                string url = "https://test.geidea.net:9090/mfstransactions/ServiceTXN";

                var jsonContent = JsonSerializer.Serialize(request);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    using JsonDocument doc = JsonDocument.Parse(responseBody);
                    if (doc.RootElement.TryGetProperty("out_parameters", out JsonElement outParams) &&
                                    outParams.TryGetProperty("GLEPayAmount", out JsonElement glePayAmount))
                    {
                        var amount = glePayAmount.GetDecimal();
                        data.current_bill_amount = (double)(amount);
                        data.external_ref_number = request.External_Ref_Number;
                        data.external_txn_id = request.External_Txn_Id;
                    }
                }
                else
                {
                    data.current_bill_amount = 0;
                    data.external_ref_number = "";
                    data.external_txn_id = "";
                }
            }
            else
            {
                data.current_bill_amount = 0;
                data.external_ref_number = "";
                data.external_txn_id = "";

            }
            return data;

        }
    }
}
