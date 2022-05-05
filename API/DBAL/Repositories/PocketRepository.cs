using Microsoft.EntityFrameworkCore;
using OnlineAuction.DBAL.Context;
using OnlineAuction.DBAL.Models;
using System;
using System.Threading.Tasks;

namespace OnlineAuction.DBAL.Repositories
{
    public class PocketRepository
    {
        private readonly OnlineAuctionContext _context;
        public PocketRepository(OnlineAuctionContext context)
        {
            _context = context;
        }

        public async Task CreateObject(Pocket pocket)
        {
            await _context.Pockets.AddAsync(pocket);
            await _context.SaveChangesAsync();
        }

        public async Task<Pocket> GetObject(Guid userGuid)
        {
            return await _context.Pockets.FirstOrDefaultAsync(x => x.HolderId == userGuid);
        }

        public async Task UpdateObject(Pocket pocket)
        {
            _context.Entry(pocket).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
