using Microsoft.EntityFrameworkCore;
using OnlineAuction.DBAL.Context;
using OnlineAuction.DBAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineAuction.DBAL.Repositories
{
    public class LotCategoryRepository
    {
        private readonly OnlineAuctionContext _context;
        private readonly Cache<IEnumerable<LotCategory>> _cache;
        public LotCategoryRepository(OnlineAuctionContext context)
        {
            _context = context;
            _cache = new Cache<IEnumerable<LotCategory>>(async () => await GetCollectionAsync(), TimeSpan.FromDays(1));
        }

        private async Task<List<LotCategory>> GetCollectionAsync() => await _context.LotCategories.ToListAsync();

        public async Task<IEnumerable<LotCategory>> GetCollection() => await _cache[string.Empty];

        public async Task<LotCategory> GetObject(Guid id) => (await _cache[string.Empty]).FirstOrDefault(x => x.Id == id);

        public async Task<LotCategory> GetObject(string name) => (await _cache[string.Empty]).FirstOrDefault(x => x.Name == name);

        public async Task CreateObject(LotCategory lotCategory)
        {
            await _context.LotCategories.AddAsync(lotCategory);
            await _context.SaveChangesAsync();
            await UpdateCache();
        }

        public async Task DeleteObject(Guid id)
        {
            _context.LotCategories.Remove(new LotCategory { Id = id });
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCache() => await _cache.Refresh(string.Empty);
    }
}
