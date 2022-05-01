using OnlineAuction.DBAL.Context;
using DBAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineAuction.DBAL.Repositories
{
	public class FinanceOperationTypeRepository
	{
    private readonly OnlineAuctionContext _context;
    private readonly Cache<IEnumerable<FinanceOperationType>> _cache;
    public FinanceOperationTypeRepository(OnlineAuctionContext context)
    {
      _context = context;
      _cache = new Cache<IEnumerable<FinanceOperationType>>(async () => await GetCollectionAsync(), TimeSpan.FromDays(1));
    }

    private async Task<IEnumerable<FinanceOperationType>> GetCollectionAsync()
    {
      return await _context.FinanceOperationTypes.ToListAsync();
    }

    public async Task<IEnumerable<FinanceOperationType>> GetCollection()
    {
      return await _cache[string.Empty];
    }

    public async Task<FinanceOperationType> GetObject(string name)
    {
      return (await _cache[string.Empty]).FirstOrDefault(x => x.Name == name);
    }
  }
}
