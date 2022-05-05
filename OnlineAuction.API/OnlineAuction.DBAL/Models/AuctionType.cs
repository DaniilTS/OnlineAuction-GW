using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineAuction.DBAL.Models
{
    public partial class AuctionType
    {
        public AuctionType()
        {
            Auctions = new HashSet<Auction>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Auction> Auctions { get; set; }
    }
}
