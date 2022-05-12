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

        public async Task<Auction> GetObject(Guid auctionId)
        {
            return await _context.Auctions.FirstOrDefaultAsync(x => x.Id == auctionId);
        }

        public async Task UpdateObject(Auction auction)
        {
            _context.Entry(auction).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task CreateObject(Auction auction)
        {
            await _context.Auctions.AddAsync(auction);
            await _context.SaveChangesAsync();
        }
    }
}
