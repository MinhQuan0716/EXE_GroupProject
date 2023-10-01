using Application.InterfaceService;
using Application.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.Text;
using System.Text.Json.Serialization;
using WebAPI.WebService;
using Application.ViewModel;
using Application.Uitls;

namespace WebAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebAPI(this IServiceCollection services, string secretKey)
        {
            services.AddScoped<IClaimService, ClaimService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICareerQuizService, CareerQuizService>();
            services.AddScoped<IVnPayService, VnPayService>();
            services.AddScoped<IExternalAuthUtils, ExternalAuthUtils>();
            services.AddHttpContextAccessor();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = secretKey,
                       ValidAudience = secretKey,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                       ClockSkew = TimeSpan.FromSeconds(1)
                   };
               });
            return services;
        }
    }
}

