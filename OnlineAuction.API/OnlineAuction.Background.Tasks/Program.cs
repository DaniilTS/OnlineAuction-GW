using Hangfire;
using Hangfire.SqlServer;
using Newtonsoft.Json;
using OnlineAuction.Background.Tasks.Jobs;
using OnlineAuction.Common.Domain.Constants;
using OnlineAuction.DBAL.Context;
using OnlineAuction.DBAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineAuction.Background.Tasks
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            GlobalConfiguration.Configuration
                                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                                .UseColouredConsoleLogProvider()
                                .UseSimpleAssemblyNameTypeSerializer()
                                .UseRecommendedSerializerSettings(opt => opt.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                                .UseSqlServerStorage("Server=DESKTOP-MD63H11;Database=OnlineAuction.Tasks;Trusted_Connection=True;", new SqlServerStorageOptions
                                {
                                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                                    QueuePollInterval = TimeSpan.Zero,
                                    UseRecommendedIsolationLevel = true,
                                    UsePageLocksOnDequeue = true,
                                    DisableGlobalLocks = true
                                });

            var onlineAuctionContext = new OnlineAuctionContext();
            var currencyPairRepository = new CurrencyPairRepository(onlineAuctionContext);

            var pair1 = await currencyPairRepository.GetObject(Currencies.USD, Currencies.BYN);
            var pair2 = await currencyPairRepository.GetObject(Currencies.RUB, Currencies.BYN);

            var currencies = new List<string> { Currencies.USD, Currencies.RUB };

            foreach (var currency in currencies) 
            {
                var pair = await currencyPairRepository.GetObject(currency, Currencies.BYN);
                RecurringJob.RemoveIfExists($"{currency}->{Currencies.BYN}");
                RecurringJob.AddOrUpdate($"{currency}->{Currencies.BYN}", () => CurrencyRateJob.Start(pair1), Crons.Each5thHour);
            }

            using (var server = new BackgroundJobServer())
            {
                Console.ReadKey();
            }
        }
    }
}
