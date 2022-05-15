using Microsoft.Extensions.DependencyInjection;
using OnlineAuction.API.Services;

namespace OnlineAuction.API.Extensions
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<AuthService>();
            services.AddScoped<AuctionService>();
            services.AddScoped<OfferService>();
            services.AddScoped<PocketService>();
            services.AddScoped<UserService>();
            services.AddScoped<LotService>();
            services.AddScoped<PhotoService>();
        }
    }
}
