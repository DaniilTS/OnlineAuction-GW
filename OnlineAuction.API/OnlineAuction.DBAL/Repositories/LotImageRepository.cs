using Microsoft.EntityFrameworkCore;
using OnlineAuction.DBAL.Context;
using OnlineAuction.DBAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineAuction.DBAL.Repositories
{
    public class LotImageRepository
    {
        private readonly OnlineAuctionContext _context;
        public LotImageRepository(OnlineAuctionContext context) 
        { 
            _context = context;
        }

        public async Task<LotImage> GetObject(Guid lotId, bool isDeleted = false)
        {
            return await _context.LotImages.FirstOrDefaultAsync(x => x.LotId == lotId && x.IsDeleted == isDeleted);
        }

        public async Task CreateObjects(IEnumerable<LotImage> lotImages) 
        {
            await _context.LotImages.AddRangeAsync(lotImages);
            await _context.SaveChangesAsync();
        }

        public async Task CreateObject(LotImage lotImage)
        {
            await _context.LotImages.AddAsync(lotImage);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateObject(LotImage lotImage)
        {
            _context.Entry(lotImage).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
