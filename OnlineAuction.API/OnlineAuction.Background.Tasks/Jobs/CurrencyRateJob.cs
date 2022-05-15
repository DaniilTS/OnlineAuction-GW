using OnlineAuction.Background.Tasks.Models;
using OnlineAuction.DBAL.Context;
using OnlineAuction.DBAL.Models;
using OnlineAuction.DBAL.Repositories;
using System;
using System.Net.Http;
using System.Text.Json;

namespace OnlineAuction.Background.Tasks.Jobs
{
    public class CurrencyRateJob
    {
        public static CurrencyPairRateRepository CurrencyRateRepository { get; set; } = new CurrencyPairRateRepository(new OnlineAuctionContext());
        public static void Start(CurrencyPair cp)
        {
            using (var httpClient = new HttpClient()) 
            {
                var streamTask = httpClient.GetStreamAsync($"https://www.nbrb.by/api/exrates/rates/{cp.From.Code}?parammode=2").Result;
                var rate = JsonSerializer.DeserializeAsync<Rate>(streamTask).Result;

                var dateTime = DateTime.UtcNow;
                Console.WriteLine($"{cp.From.Code}->{cp.To.Code}: {rate.Cur_OfficialRate} on {dateTime}");

                _ = CurrencyRateRepository.CreateObject(new()
                {
                    Id = Guid.NewGuid(),
                    CurrencyPairId = cp.Id,
                    Rate = rate.Cur_OfficialRate,
                    RateTime = dateTime
                });
            }
        }
    }
}
