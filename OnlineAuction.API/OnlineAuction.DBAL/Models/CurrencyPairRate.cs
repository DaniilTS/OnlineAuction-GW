using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace OnlineAuction.DBAL.Models
{
    public partial class CurrencyPairRate
    {
        public Guid Id { get; set; }
        public Guid CurrencyPairId { get; set; }
        public decimal Rate { get; set; }
        public DateTime RateTime { get; set; }
        public virtual CurrencyPair CurrencyPair { get; set; }
    }
}
