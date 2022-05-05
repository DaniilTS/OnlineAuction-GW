using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineAuction.DBAL.Models
{
    public partial class CurrencyPairRate
    {
        public Guid Id { get; set; }
        public Guid CurrencyPairRateId { get; set; }
        public decimal Rate { get; set; }
        public DateTime RateTime { get; set; }

        public virtual CurrencyPair CurrencyPairRateNavigation { get; set; }
    }
}
