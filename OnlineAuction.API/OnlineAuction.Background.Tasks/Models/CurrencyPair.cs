using System;

namespace OnlineAuction.Background.Tasks.Models
{
    public class CurrencyPair
    {
        public Guid Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}
