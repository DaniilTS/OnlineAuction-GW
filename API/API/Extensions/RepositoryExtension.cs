using OnlineAuction.DBAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineAuction.API.Extensions
{
    public static class RepositoryExtension
    {
        public static void AddRepositories(this IServiceCollection services) 
        {
            services.AddScoped<AuctionTypeRepository>();
            services.AddScoped<BalanceOperationTypeRepository>();
            services.AddScoped<FinanceOperationTypeRepository>();
            services.AddScoped<GenderRepository>();
            services.AddScoped<OfferStatusRepository>();
            services.AddScoped<RoleRepository>();
        }
    }
}
