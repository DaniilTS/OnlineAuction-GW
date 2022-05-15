using Microsoft.EntityFrameworkCore;
using OnlineAuction.DBAL.Context;
using OnlineAuction.DBAL.Models;
using System.Threading.Tasks;

namespace OnlineAuction.DBAL.Repositories
{
    public class OfferRepository
    {
        private readonly OnlineAuctionContext _context;
        public OfferRepository(OnlineAuctionContext context)
        { 
            _context = context;
        }

        public async Task UpdateObject(Offer offer)
        {
            _context.Entry(offer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task CreateObject(Offer lot)
        {
            await _context.Offers.AddAsync(lot);
            await _context.SaveChangesAsync();
        }
    }
}
