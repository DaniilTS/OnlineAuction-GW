using Microsoft.Extensions.DependencyInjection;
using OnlineAuction.API.Models.Requests;
using OnlineAuction.DBAL.Models;
using OnlineAuction.DBAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineAuction.API.Services
{
    public class AuctionService
    {
        private readonly AuctionRepository auctionRepository;
        private readonly AuctionLogRepository auctionLogRepository;
        public AuctionService(IServiceProvider sp) 
        { 
            auctionRepository = sp.GetService<AuctionRepository>();
            auctionLogRepository = sp.GetService<AuctionLogRepository>();
        }

        public IEnumerable<object> GetAuctionLogs(Guid auctionId)
        {
            return auctionLogRepository.GetCollection(auctionId);
        }

        public async Task CreateAuction(AuctionCreateRequest request) 
        {
            var auction = new Auction
            {
                Id = Guid.NewGuid(),
                AuctionTypeId = request.AuctionTypeId,
                LotId = request.LotId,
                Start = request.Start,
                End = request.Start.AddDays(request.Duration),
                StartPrice = request.StartPrice,
                EndPrice = request.StartPrice,
                Commission = null,
                FinanceOperationId = null,
                CommissionFinanceOperationId = null,
                IsStarted = false,
                IsFinished = false,
                IsEmailMessageSended = false,
                Created = DateTime.UtcNow
            };

            await auctionRepository.CreateObject(auction);
        }

        public async Task ApplyBid(Guid userId, Guid auctionId) 
        {
            var auction = await auctionRepository.GetObject(auctionId);
            auction.EndPrice = auction.StartPrice;
            auction.Commission = auction.EndPrice * 0.05m;
            auction.WinnerId = userId;

            await auctionRepository.UpdateObject(auction);
            await AuctionLog(auctionId, userId, "Applied first bid");
        }

        public async Task RaiseBid(Guid userId, Guid auctionId, AuctionRaiseBidRequest request) 
        {
            var auction = await auctionRepository.GetObject(auctionId);
            var startEndPrice = auction.EndPrice;
            auction.EndPrice += request.RaiseAmount;
            auction.Commission = auction.EndPrice * 0.05m;
            auction.WinnerId = userId;

            await auctionRepository.UpdateObject(auction);
            await AuctionLog(auctionId, userId, $"Raised lot price from {startEndPrice} up to {auction.EndPrice}");
        }

        public async Task MakeBid(Guid userId, Guid auctionId, AuctionMakeBidRequest request) 
        {
            var auction = await auctionRepository.GetObject(auctionId);
            if (request.Amount > auction.EndPrice) 
            {
                auction.EndPrice = request.Amount;
                auction.Commission = auction.EndPrice * 0.05m;
                auction.WinnerId = userId;

                await auctionRepository.UpdateObject(auction);
            }

            await AuctionLog(auctionId, userId, $"Applied bid in amount of {request.Amount}");
        }

        private async Task AuctionLog(Guid auctionId, Guid userId, string logText) 
        {
            await auctionLogRepository.CreateObject(new()
            {
                Id = Guid.NewGuid(),
                AuctionId = auctionId,
                UserId = userId,
                Action = logText,
                Created = DateTime.UtcNow
            });
        }
    }
}
