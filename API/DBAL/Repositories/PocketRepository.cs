using OnlineAuction.DBAL.Context;
using OnlineAuction.DBAL.Models;

namespace OnlineAuction.DBAL.Repositories
{
    public class PocketRepository: GenericRepository<Pocket>
    {
        private readonly OnlineAuctionContext _context;
        public PocketRepository(OnlineAuctionContext context): base(context)
        {
        }
    }
}
