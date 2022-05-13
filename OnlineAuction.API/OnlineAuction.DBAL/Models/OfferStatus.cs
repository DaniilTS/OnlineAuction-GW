using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineAuction.DBAL.Models
{
    public partial class OfferStatus
    {
        public OfferStatus()
        {
            Offers = new HashSet<Offer>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }
    }
}
