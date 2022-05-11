using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OnlineAuction.Auth.Helpers;
using OnlineAuction.Auth.Models;
using OnlineAuction.Auth.Services;

namespace OnlineAuction.Auth.Extensions
{
    public static class AuthExtension
    {
        public static void AddJwtBearerAuth(this IServiceCollection services, IConfiguration Configuration)
        {
            var authSettings = new AuthSettings();
            Configuration.GetSection(nameof(AuthSettings)).Bind(authSettings);

            services.AddSingleton(authSettings);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authSettings.Issuer,
                        ValidateAudience = true,
                        ValidAudience = authSettings.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = KeyHelper.GetSymmetricSecurityKey(authSettings.Key),
                        ValidateIssuerSigningKey = true
                    };
                });

            services.AddScoped<TokenService>();
        }
    }
}
