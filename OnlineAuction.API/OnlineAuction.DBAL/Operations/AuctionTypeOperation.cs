using OnlineAuction.Common.Domain.Constants;
using OnlineAuction.DBAL.Models;
using OnlineAuction.DBAL.Repositories;
using System.Collections.Generic;

namespace OnlineAuction.DBAL.Operations
{
    public class AuctionTypeOperation
    {
        private readonly AuctionTypeRepository _repo;

        public AuctionTypeOperation(AuctionTypeRepository repo)
        {
            _repo = repo;
        }

        public AuctionType Opened => _repo.GetObject(AuctionTypes.Opened).Result;
        public AuctionType Closed => _repo.GetObject(AuctionTypes.Closed).Result;
        public List<AuctionType> Types => new() { Opened, Closed };
    }
}
