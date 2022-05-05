using Microsoft.EntityFrameworkCore;
using OnlineAuction.DBAL.Context;
using System;
using System.Threading.Tasks;

namespace OnlineAuction.DBAL.Repositories
{
    public class GenericRepository<T> where T : class, IBaseDbModelInterface
    {
        private readonly OnlineAuctionContext _context;
        public GenericRepository(OnlineAuctionContext context)
        {
            _context = context;
        }

        public async Task CreateObject(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
