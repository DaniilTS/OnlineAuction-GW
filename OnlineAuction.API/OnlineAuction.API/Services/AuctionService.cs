using Microsoft.Extensions.DependencyInjection;
using OnlineAuction.DBAL.Repositories;
using System;
using System.Threading.Tasks;

namespace OnlineAuction.API.Services
{
    public class AuctionService
    {
        private readonly AuctionRepository auctionRepository;
        public AuctionService(IServiceProvider sp) 
        { 
            auctionRepository = sp.GetService<AuctionRepository>();
        }

        public async Task CreateAuction() 
        { 
        
        }
    }
}
