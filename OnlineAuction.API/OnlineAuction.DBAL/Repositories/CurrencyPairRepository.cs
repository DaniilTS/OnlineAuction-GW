using Microsoft.EntityFrameworkCore;
using OnlineAuction.DBAL.Context;
using OnlineAuction.DBAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineAuction.DBAL.Repositories
{
    public class CurrencyPairRepository
    {
        private readonly OnlineAuctionContext _context;
        private readonly Cache<List<CurrencyPair>> _cache;
        public CurrencyPairRepository(OnlineAuctionContext context) 
        {
            _context = context;
            _cache = new Cache<List<CurrencyPair>>(async () => await GetCollectionAsync(), TimeSpan.FromDays(1));
        }

        private async Task<List<CurrencyPair>> GetCollectionAsync() 
        {
            return await _context.CurrencyPairs
                .Include(x => x.From)
                .Include(x => x.To)
                .ToListAsync();
        }

        public async Task<List<CurrencyPair>> GetCollection() 
        {
            return await _cache[string.Empty];
        }

        public async Task<CurrencyPair> GetObject(string fromCode, string toCode) 
        {
            return (await GetCollection()).FirstOrDefault(x => x.From.Code == fromCode && x.To.Code == toCode);
        }
    }
}
