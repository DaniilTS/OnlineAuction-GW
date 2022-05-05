﻿using OnlineAuction.DBAL.Context;
using System.Threading.Tasks;
using OnlineAuction.DBAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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

        public async Task<User> GetObject(string email, bool isDeleted = false) 
        {
            return await _context.Users.Where(x => x.IsDeleted == isDeleted)
                .Include(x => x.Role).FirstOrDefaultAsync(u => u.Email == email);
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
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Phone == phone);
            return user != null;
        }
    }
}
