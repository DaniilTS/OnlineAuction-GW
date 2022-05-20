using Microsoft.EntityFrameworkCore;
using OnlineAuction.DBAL.Context;
using OnlineAuction.DBAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineAuction.DBAL.Repositories
{
    public class LotRepository
    {
        public OnlineAuctionContext _context;
        public LotRepository(OnlineAuctionContext context)
        {
            _context = context;
        }

        public async Task<List<Lot>> GetCollection() 
        {
            return await _context.Lots
                .Include(x => x.LotCategory)
                .Include(x => x.Creator)
                    .ThenInclude(c => c.FullNameId)
                .Include(x => x.LotImages)
                .ToListAsync();
        }

        public async Task<Lot> GetObject(Guid lotId) 
        {
            return await _context.Lots.FirstOrDefaultAsync(x => x.Id == lotId);
        }

        public async Task UpdateObject(Lot lot) 
        {
            _context.Entry(lot).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task CreateObject(Lot lot)
        {
            await _context.Lots.AddAsync(lot);
            await _context.SaveChangesAsync();
        }
    }
}
