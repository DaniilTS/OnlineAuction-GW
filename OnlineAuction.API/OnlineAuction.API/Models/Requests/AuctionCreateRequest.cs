using System;

namespace OnlineAuction.API.Models.Requests
{
    public class AuctionCreateRequest
    {
        public Guid AuctionTypeId { get; set; }
        public Guid LotId { get; set; }
        public DateTime Start { get; set; }
        public int Duration { get; set; }
        public decimal StartPrice { get; set; }
    }
}
