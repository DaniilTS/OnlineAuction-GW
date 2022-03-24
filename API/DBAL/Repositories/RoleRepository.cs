using DBAL.Context;
using DBAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBAL.Repositories
{
    public class RoleRepository
    {
        private readonly OnlineAuctionContext _context;
        private readonly Cache<IEnumerable<Role>> _cache;
        public RoleRepository(OnlineAuctionContext context) 
        {
            _context = context;
            _cache = new Cache<IEnumerable<Role>>(async () => await GetCollectionAsync(), TimeSpan.FromDays(1));
        }

        private async Task<IEnumerable<Role>> GetCollectionAsync() 
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetCollection() 
        {
            return await _cache[string.Empty];
        }

        public async Task<Role> GetObject(string name) 
        {
            var res = (await _cache[string.Empty]).ToList();
            return res.FirstOrDefault(x => x.Name == name);
        }
    }
}
