using Microsoft.EntityFrameworkCore;
using OnlineAuction.DBAL.Context;
using OnlineAuction.DBAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineAuction.DBAL.Repositories
{
    public class AuctionLogRepository
    {
        private readonly OnlineAuctionContext _context;
        public AuctionLogRepository(OnlineAuctionContext context) 
        { 
            _context = context;
        }

        public IEnumerable<object> GetCollection(Guid auctionId) 
        {
            return _context.AuctionLogs.Include(x => x.User).ThenInclude(u => u.FullName).Where(x => x.AuctionId == auctionId)
                .Select(x => new
                { 
                    x.Id,
                    userFullName = $"{x.User.FullName.FirstName} {x.User.FullName.ThirdName}".TrimEnd(),
                    x.Action,
                    x.Created
                }).AsEnumerable().Select(x => new 
                { 
                    Id = x.Id,
                    Action = x.Action,
                    Name = x.userFullName,
                    Created = x.Created
                });
        }

        public async Task CreateObject(AuctionLog log) 
        {
            await _context.AuctionLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }
    }
}
