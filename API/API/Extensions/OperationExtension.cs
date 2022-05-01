using OnlineAuction.DBAL.Operations;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineAuction.API.Extensions
{
    public static class OperationExtension
    {
        public static void AddOperations(this IServiceCollection services)
        {
            services.AddScoped<AuctionTypeOperation>();
            services.AddScoped<BalanceOperationTypeOperation>();
            services.AddScoped<FinanceOperationTypeOperation>();
            services.AddScoped<GenderOperation>();
            services.AddScoped<OfferStatusOperation>();
            services.AddScoped<RoleOperation>();
        }
    }
}
