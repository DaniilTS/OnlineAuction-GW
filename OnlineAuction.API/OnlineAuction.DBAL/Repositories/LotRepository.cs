using OnlineAuction.DBAL.Context;
using OnlineAuction.DBAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.DBAL.Repositories
{
    public class LotRepository
    {
        public OnlineAuctionContext _context;
        public LotRepository(OnlineAuctionContext context)
        {
            _context = context;
        }

        public async Task CreateObject(Lot lot)
        {
            await _context.Lots.AddAsync(lot);
            await _context.SaveChangesAsync();
        }
    }
}
