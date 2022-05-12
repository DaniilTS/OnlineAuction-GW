using Microsoft.AspNetCore.Mvc;
using System;

namespace OnlineAuction.API.Models.Requests
{
    public class AuctionRaiseBidRequest
    {
        public int RaiseAmount { get; set; }
    }
}
