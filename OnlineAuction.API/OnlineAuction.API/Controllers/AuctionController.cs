using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineAuction.API.Services;
using System;
using System.Threading.Tasks;

namespace OnlineAuction.API.Controllers
{
    public class AuctionController: BaseController
    {
        private readonly AuctionService _auctionService;
        public AuctionController(AuctionService auctionService, IServiceProvider sp) : base(sp)
        { 
            _auctionService = auctionService;
        }

        [HttpPost("")]
        public Task<IActionResult> CreateAuction() 
        {
            return null;
        }
    }
}
