using Microsoft.EntityFrameworkCore;
using OnlineAuction.DBAL.Context;
using OnlineAuction.DBAL.Models;
using System;
using System.Threading.Tasks;

namespace OnlineAuction.DBAL.Repositories
{
    public class UserImageRepository
    {
        private readonly OnlineAuctionContext _context;
        public UserImageRepository(OnlineAuctionContext context)
        {
            _context = context;
        }

        public async Task<UserImage> GetObject(Guid userGuid)
        {
            return await _context.UserImages.FirstOrDefaultAsync(x => x.UserId == userGuid);
        }

        public async Task CreateObject(UserImage userImage)
        {
            await _context.UserImages.AddAsync(userImage);
            await _context.SaveChangesAsync();
        }
    }
}
