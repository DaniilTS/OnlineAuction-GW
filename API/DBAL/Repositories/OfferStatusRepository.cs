using OnlineAuction.DBAL.Context;
using OnlineAuction.DBAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineAuction.DBAL.Repositories
{
	public class OfferStatusRepository
	{
    private readonly OnlineAuctionContext _context;
    private readonly Cache<IEnumerable<OfferStatus>> _cache;
    public OfferStatusRepository(OnlineAuctionContext context)
    {
      _context = context;
      _cache = new Cache<IEnumerable<OfferStatus>>(async () => await GetCollectionAsync(), TimeSpan.FromDays(1));
    }

    private async Task<IEnumerable<OfferStatus>> GetCollectionAsync()
    {
      return await _context.OfferStatuses.ToListAsync();
    }

    public async Task<IEnumerable<OfferStatus>> GetCollection()
    {
      return await _cache[string.Empty];
    }

    public async Task<OfferStatus> GetObject(string name)
    {
      return (await _cache[string.Empty]).FirstOrDefault(x => x.Name == name);
    }
  }
}
