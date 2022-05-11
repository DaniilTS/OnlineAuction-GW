using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineAuction.DBAL.Models
{
    public partial class Lot
    {
        public Lot()
        {
            Auctions = new HashSet<Auction>();
            LotImages = new HashSet<LotImage>();
            Offers = new HashSet<Offer>();
        }

        public Guid Id { get; set; }
        public Guid LotCategoryId { get; set; }
        public string Description { get; set; }
        public Guid CreatorId { get; set; }
        public bool IsSubmitted { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual User Creator { get; set; }
        public virtual LotCategory LotCategory { get; set; }
        public virtual ICollection<Auction> Auctions { get; set; }
        public virtual ICollection<LotImage> LotImages { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }
    }
}
