using Microsoft.EntityFrameworkCore;
using OnlineAuction.DBAL.Context;
using OnlineAuction.DBAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.DBAL.Repositories
{
    public class CurrencyRepository
    {
        private readonly OnlineAuctionContext _context;
        private readonly Cache<IEnumerable<Currency>> _cache;
        public CurrencyRepository(OnlineAuctionContext context)
        {
            _context = context;
            _cache = new Cache<IEnumerable<Currency>>(async () => await GetCollectionAsync(), TimeSpan.FromDays(1));
        }

        private async Task<IEnumerable<Currency>> GetCollectionAsync() 
        {
            return await _context.Currencies.ToListAsync();
        }

        public async Task<IEnumerable<Currency>> GetCollection() 
        {
            return await _cache[string.Empty];
        }
    }
}
