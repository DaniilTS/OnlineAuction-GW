﻿using OnlineAuction.DBAL.Context;
using OnlineAuction.DBAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineAuction.DBAL.Repositories
{
	public class BalanceOperationTypeRepository
	{
    private readonly OnlineAuctionContext _context;
    private readonly Cache<IEnumerable<BalanceOperationType>> _cache;
    public BalanceOperationTypeRepository(OnlineAuctionContext context)
    {
      _context = context;
      _cache = new Cache<IEnumerable<BalanceOperationType>>(async () => await GetCollectionAsync(), TimeSpan.FromDays(1));
    }
    private async Task<IEnumerable<BalanceOperationType>> GetCollectionAsync() 
    {
      return await _context.BalanceOperationTypes.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<BalanceOperationType>> GetCollection() 
    {
      return await _cache[string.Empty];
    }

    public async Task<BalanceOperationType> GetObject(string name) 
    {
      return (await _cache[string.Empty]).FirstOrDefault(x => x.Name == name);
    }
  }
}
