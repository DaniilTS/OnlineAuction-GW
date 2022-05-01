using DBAL.Context;
using DBAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBAL.Repositories
{
    public class AuctionTypeRepository
    {
        private readonly OnlineAuctionContext _context;
        private readonly Cache<IEnumerable<AuctionType>> _cache;
        public AuctionTypeRepository(OnlineAuctionContext context)
        {
            _context = context;
            _cache = new Cache<IEnumerable<AuctionType>>(async () => await GetCollectionAsync(), TimeSpan.FromDays(1));
        }

        private async Task<IEnumerable<AuctionType>> GetCollectionAsync()
        {
            return await _context.AuctionTypes.ToListAsync();
        }

        public async Task<IEnumerable<AuctionType>> GetCollection()
        {
            return await _cache[string.Empty];
        }

        public async Task<AuctionType> GetObject(string name)
        {
            return (await _cache[string.Empty]).FirstOrDefault(x => x.Name == name);
        }
    }
}
