using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineAuction.DBAL.Models
{
    public partial class Currency
    {
        public Currency()
        {
            CurrencyPairFroms = new HashSet<CurrencyPair>();
            CurrencyPairTos = new HashSet<CurrencyPair>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<CurrencyPair> CurrencyPairFroms { get; set; }
        public virtual ICollection<CurrencyPair> CurrencyPairTos { get; set; }
    }
}
