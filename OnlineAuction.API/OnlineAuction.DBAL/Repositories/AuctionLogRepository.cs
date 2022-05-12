using OnlineAuction.DBAL.Context;
using OnlineAuction.DBAL.Models;
using System.Threading.Tasks;

namespace OnlineAuction.DBAL.Repositories
{
    public class AuctionLogRepository
    {
        private readonly OnlineAuctionContext _context;
        public AuctionLogRepository(OnlineAuctionContext context) 
        { 
            _context = context;
        }

        public async Task CreateObject(AuctionLog log) 
        {
            await _context.AuctionLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }
    }
}
