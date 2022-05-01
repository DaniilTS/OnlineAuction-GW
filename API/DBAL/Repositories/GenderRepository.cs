using OnlineAuction.DBAL.Context;
using OnlineAuction.DBAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineAuction.DBAL.Repositories
{
    public class GenderRepository
    {
        private readonly OnlineAuctionContext _context;
        private readonly Cache<IEnumerable<Gender>> _cache;
        public GenderRepository(OnlineAuctionContext context)
        {
            _context = context;
            _cache = new Cache<IEnumerable<Gender>>(async () => await GetCollectionAsync(), TimeSpan.FromDays(1));
        }

        private async Task<IEnumerable<Gender>> GetCollectionAsync()
        {
            return await _context.Genders.ToListAsync();
        }

        public async Task<IEnumerable<Gender>> GetCollection()
        {
            return await _cache[string.Empty];
        }

        public async Task<Gender> GetObject(string name)
        {
            return (await _cache[string.Empty]).FirstOrDefault(x => x.Name == name);
        }
    }
}
