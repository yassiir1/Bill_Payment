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
            var path = context.Request.Path.Value?.ToLower();

            // تخطي أنواع الملفات الثابتة
            if (path != null && (
                path.EndsWith(".jpg") || path.EndsWith(".jpeg") ||
                path.EndsWith(".png") || path.EndsWith(".gif") ||
                path.EndsWith(".webp") || path.EndsWith(".svg") ||
                path.EndsWith(".js") || path.EndsWith(".css") ||
                path.EndsWith(".ico") || path.EndsWith(".woff") ||
                path.EndsWith(".woff2") || path.EndsWith(".ttf") ||
                path.EndsWith(".eot") || path.EndsWith(".map")
            ))
            {
                await _next(context);
                return;
            }

            // تخطي export endpoint
            if (context.Request.Path.StartsWithSegments("/api/users/export"))
            {
                await _next(context);
                return;
            }

            // استثناء endpoint رفع البانر (لأنه بيستقبل FormData ويرجع رد ممكن مش JSON)
            if (context.Request.Path.StartsWithSegments("/api/ui-settings/addbanner"))
            {
                await _next(context);
                return;
            }

            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
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

            object? responseData = null;

            if (!string.IsNullOrWhiteSpace(responseText))
            {
                // تحقق أن Content-Type موجود وهو JSON
                if (context.Response.ContentType != null && context.Response.ContentType.Contains("application/json"))
                {
                    try
                    {
                        responseData = JsonSerializer.Deserialize<Dictionary<string, object>>(responseText);
                    }
                    catch
                    {
                        // لو مش JSON صالح نرجع الرد كما هو بدون تغليف
                        context.Response.Body = originalBodyStream;
                        await context.Response.WriteAsync(responseText);
                        return;
                    }
                }
                else
                {
                    // لو مش JSON، نرجع الرد كما هو بدون تغليف
                    context.Response.Body = originalBodyStream;
                    await context.Response.WriteAsync(responseText);
                    return;
                }
            }

            object? finalData = null;
            string? message = "Success";

            if (responseData is Dictionary<string, object> dict)
            {
                finalData = dict.ContainsKey("data") ? dict["data"] : null;
                message = dict.ContainsKey("message") ? dict["message"]?.ToString() : message;
            }

            var apiResponse = new
            {
                data = finalData,
                message = message,
                status = context.Response.StatusCode == 200 ? "success" : "error"
            };

            context.Response.Body = originalBodyStream;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(apiResponse));
        }
    }
}
