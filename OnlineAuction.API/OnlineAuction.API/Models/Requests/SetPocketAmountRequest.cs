using System;

namespace OnlineAuction.API.Models.Requests
{
    public class SetPocketAmountRequest
    {
        public Guid PocketGuid { get; set; }
        public decimal PocketAmount { get; set; }
    }
}
