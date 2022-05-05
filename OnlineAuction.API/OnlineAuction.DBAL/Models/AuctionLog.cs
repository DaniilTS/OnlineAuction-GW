using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineAuction.DBAL.Models
{
    public partial class AuctionLog
    {
        public Guid Id { get; set; }
        public Guid AuctionId { get; set; }
        public Guid UserId { get; set; }
        public string Action { get; set; }
        public DateTime Created { get; set; }
    }
}
