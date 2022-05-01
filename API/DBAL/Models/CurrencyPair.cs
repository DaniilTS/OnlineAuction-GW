using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineAuction.DBAL.Models
{
    public partial class CurrencyPair
    {
        public CurrencyPair()
        {
            CurrencyPairRates = new HashSet<CurrencyPairRate>();
        }

        public Guid Id { get; set; }
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }

        public virtual Currency From { get; set; }
        public virtual Currency To { get; set; }
        public virtual ICollection<CurrencyPairRate> CurrencyPairRates { get; set; }
    }
}
