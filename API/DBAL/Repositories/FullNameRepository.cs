using Microsoft.EntityFrameworkCore;
using OnlineAuction.DBAL.Context;
using OnlineAuction.DBAL.Models;
using System;
using System.Threading.Tasks;

namespace OnlineAuction.DBAL.Repositories
{
    public class FullNameRepository
    {
        private readonly OnlineAuctionContext _context;
        public FullNameRepository(OnlineAuctionContext context)
        {
            _context = context;
        }

        public async Task<FullName> GetOrCreate(FullName fullName) 
        {
            var fn = await GetObject(fullName);
            return fn is null 
                ? await CreateObject(fullName) 
                : fn;
        }

        public async Task<FullName> GetObject(FullName fullName) 
        {
            return await _context.FullNames.FirstOrDefaultAsync(x => x.FirstName == fullName.FirstName &&
                                                                     x.SecondName == fullName.SecondName &&
                                                                     x.ThirdName == fullName.ThirdName);
        }

        public async Task<FullName> CreateObject(FullName fullName) 
        {
            fullName.Id = Guid.NewGuid();
            await _context.FullNames.AddAsync(fullName);
            await _context.SaveChangesAsync();

            return fullName;
        }
    }
}
