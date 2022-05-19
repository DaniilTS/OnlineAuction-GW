using OnlineAuction.DBAL.Context;
using System.Threading.Tasks;
using OnlineAuction.DBAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace OnlineAuction.DBAL.Repositories
{
    public class UserRepository
    {
        private readonly OnlineAuctionContext _context;
        public UserRepository(OnlineAuctionContext context)
        {
            _context = context;
        }

        public async Task<User> GetObject(Guid id, bool isDeleted = false)
        {
            return await _context.Users.Where(x => x.IsDeleted == isDeleted).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetColelction(PaginationParams pagination, bool isDeleted = false) 
        {
            return await _context.Users.Where(x => x.IsDeleted == isDeleted)
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize).ToListAsync();
        }

        public async Task<User> GetObject(string email, bool isDeleted = false) 
        {
            return await _context.Users
                .Where(x => x.IsDeleted == isDeleted)
                .Include(x => x.Role)
                .Include(x => x.FullName)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task CreateObject(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateObject(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsObjectExists(string email, string phone)
        {
            return await _context.Users.AnyAsync(u => u.Email == email && u.Phone == phone);
        }
    }
}
