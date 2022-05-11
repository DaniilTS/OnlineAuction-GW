using Microsoft.EntityFrameworkCore;
using OnlineAuction.DBAL.Context;
using OnlineAuction.DBAL.Models;
using System;
using System.Threading.Tasks;

namespace OnlineAuction.DBAL.Repositories
{
    public class AuctionRepository
    {
        private readonly OnlineAuctionContext _context;
        public AuctionRepository(OnlineAuctionContext context) 
        {
            _context = context;
        }

        public async Task<Auction> GetObject(Guid lotId)
        {
            return await _context.Auctions.FirstOrDefaultAsync(x => x.Id == lotId);
        }

        public async Task UpdateObject(Auction lot)
        {
            _context.Entry(lot).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task CreateObject(Auction lot)
        {
            await _context.Auctions.AddAsync(lot);
            await _context.SaveChangesAsync();
        }
    }
}
