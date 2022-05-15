using OnlineAuction.Background.Tasks.Models;
using OnlineAuction.DBAL.Repositories;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineAuction.Background.Tasks.Jobs
{
    public class CurrencyRateJob
    {
        public static async Task Start(DBAL.Models.CurrencyPair cp, CurrencyPairRateRepository currencyPairRateRepository) 
        {
            using (var httpClient = new HttpClient()) 
            {
                var streamTask = httpClient.GetStreamAsync($"https://www.nbrb.by/api/exrates/rates/{cp.From.Code}?parammode=2");
                var rate = await JsonSerializer.DeserializeAsync<Rate>(await streamTask);

                Console.WriteLine($"{cp.From.Code}->{cp.To.Code}: {rate.Cur_OfficialRate}");

                await currencyPairRateRepository.CreateObject(new()
                {
                    Id = Guid.NewGuid(),
                    CurrencyPairId = cp.Id,
                    Rate = rate.Cur_OfficialRate,
                    RateTime = DateTime.UtcNow
                });
            }     
        }
    }
}
