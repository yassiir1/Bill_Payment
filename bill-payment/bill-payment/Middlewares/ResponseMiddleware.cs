using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace bill_payment.Middlewares
{
    public class ResponseMiddleware
    {
        private readonly RequestDelegate _next;
        public ResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                try
                {
                    await _next(context); 
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    var errorResponse = new
                    {
                        message = ex.Message, 
                        status = "error"
                    };

                    context.Response.Body = originalBodyStream;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
                    return;
                }

                responseBody.Seek(0, SeekOrigin.Begin);
                var responseText = await new StreamReader(responseBody).ReadToEndAsync();
                var responseData = responseText.Length > 0 ? JsonSerializer.Deserialize<Dictionary<string, object>>(responseText) : null;

                var apiResponse = new
                {
                    data = responseData != null && responseData.ContainsKey("data") ? responseData["data"] : null,
                    message = responseData != null && responseData.ContainsKey("message") ? responseData["message"] : (context.Response.StatusCode == 200 ? "Success" : "An error occurred"),
                    status = context.Response.StatusCode == 200 ? "success" : "error"
                };

                context.Response.Body = originalBodyStream;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(apiResponse));
            }
        }
    }
}
