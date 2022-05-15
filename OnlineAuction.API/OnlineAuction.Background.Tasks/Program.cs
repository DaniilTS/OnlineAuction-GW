using Hangfire;
using Hangfire.SqlServer;
using Newtonsoft.Json;
using OnlineAuction.Background.Tasks.Jobs;
using OnlineAuction.DBAL.Context;
using OnlineAuction.DBAL.Repositories;
using System;
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
            var currencyPairRateRepository = new CurrencyPairRateRepository(onlineAuctionContext);

            var pair1 = await currencyPairRepository.GetObject("USD", "BYN");
            await CurrencyRateJob.Start(pair1, currencyPairRateRepository);

            //RecurringJob.AddOrUpdate(() => Console.WriteLine("Recurring job!"), Cron.Minutely);
            //RecurringJob.AddOrUpdate(() => CurrencyRateJob.Start(pair1, currencyPairRateRepository), Cron.Minutely);

            using (var server = new BackgroundJobServer())
            {
                Console.ReadKey();
            }
        }
    }
}
