using bill_payment.BillDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Keycloak.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using bill_payment.InterfacesService;
using bill_payment.ImplementService;
using bill_payment.Middlewares;
using bill_payment.MobileAppServices;
using bill_payment.ServicesExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient(); // Register IHttpClientFactory
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddApplicationServices();


builder.Services.AddDbContext<Bill_PaymentContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("BillPaymentDbContext")));


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = "http://localhost:8080/auth/realms/bill-payment-sdk";
    //options.Authority = "http://127.0.0.1:8280/auth/realms/bill-payment-sdk";
    options.Audience = "account";
    options.RequireHttpsMetadata = false; 


    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "http://localhost:8080/auth/realms/bill-payment-sdk",
        //ValidIssuer = "http://127.0.0.1:8280/auth/realms/bill-payment-sdk",
        ValidAudience = "account",
        RoleClaimType = ClaimTypes.Role
    };

    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            var claims = context.Principal.Claims.ToList();

            var resourceAccess = context.Principal.FindFirst("resource_access")?.Value;
            if (!string.IsNullOrEmpty(resourceAccess))
            {
                var parsedResourceAccess = JObject.Parse(resourceAccess);
                var roles = parsedResourceAccess["bill-payment-sdk-service"]?["roles"]?.ToObject<List<string>>();

                foreach (var role in roles)
                {
                    ((ClaimsIdentity)context.Principal.Identity).AddClaim(new Claim(ClaimTypes.Role, role));
                }
            }

            return Task.CompletedTask;
        }
    };
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() 
              .AllowAnyHeader() 
              .AllowAnyMethod();
    });
});
var app = builder.Build();
app.UseCors("AllowAll");
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<Bill_PaymentContext>();
    dbContext.Database.Migrate(); 
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ResponseMiddleware>();
app.UseMiddleware<UserIdMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();
app.MapControllers();

app.Run();
