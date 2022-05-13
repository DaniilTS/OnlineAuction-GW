using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineAuction.API.Models.Requests;
using OnlineAuction.API.Services;
using System;
using System.Threading.Tasks;

namespace OnlineAuction.API.Controllers
{
    public class AuctionController : BaseController
    {
        private readonly AuctionService _auctionService;
        public AuctionController(AuctionService auctionService, IServiceProvider sp) : base(sp)
        {
            _auctionService = auctionService;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateAuction([FromForm] AuctionCreateRequest request)
        {
            await _auctionService.CreateAuction(request);
            return Ok();
        }

        [HttpPost("{id}/applyBid")]
        [Authorize]
        public async Task<IActionResult> ApplyBid(Guid id)
        {
            await _auctionService.ApplyBid(CurrentUser.Id, id);
            return Ok();
        }

        [HttpPost("{id:Guid}/raiseBid")]
        [Authorize]
        public async Task<IActionResult> RaiseBid(Guid id, [FromForm] AuctionRaiseBidRequest request)//is for opened auction
        {
            await _auctionService.RaiseBid(CurrentUser.Id, id, request);
            return Ok();
        }

        [HttpPost("{id:Guid}/makeBid")]
        [Authorize]
        public async Task<IActionResult> MakeBid(Guid id, [FromForm] AuctionMakeBidRequest request)//is for closed auction 
        {
            await _auctionService.MakeBid(CurrentUser.Id, id, request);
            return Ok();
        }

        [HttpGet("{id:Guid}/logs")]
        public async Task<IActionResult> GetAuctionLog(Guid id) 
        {
            return Ok(_auctionService.GetAuctionLogs(id));
        }
    }
}
