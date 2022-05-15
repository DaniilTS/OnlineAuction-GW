using Microsoft.EntityFrameworkCore;
using OnlineAuction.DBAL.Context;
using OnlineAuction.DBAL.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineAuction.DBAL.Repositories
{
    public class CurrencyPairRateRepository
    {
        private readonly OnlineAuctionContext _context;
        public CurrencyPairRateRepository(OnlineAuctionContext context) 
        {
            _context = context;
        }

        public async Task<CurrencyPairRate> GetObject(Guid currencyPairId) 
        {
            return await _context.CurrencyPairRates
                .Where(x => x.CurrencyPairId == currencyPairId)
                .OrderByDescending(x => x.RateTime)
                .FirstOrDefaultAsync();
        }

        public async Task CreateObject(CurrencyPairRate currencyPairRate) 
        {
            await _context.CurrencyPairRates.AddAsync(currencyPairRate);
            await _context.SaveChangesAsync();
        }
    }
}
