using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineAuction.DBAL.Models
{
    public partial class FinanceOperation
    {
        public FinanceOperation()
        {
            AuctionCommissionFinanceOperations = new HashSet<Auction>();
            AuctionFinanceOperations = new HashSet<Auction>();
            Offers = new HashSet<Offer>();
        }

        public Guid Id { get; set; }
        public Guid PocketId { get; set; }
        public Guid FinanceOperationTypeId { get; set; }
        public Guid FinanceOperationStatusId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual FinanceOperationStatus FinanceOperationStatus { get; set; }
        public virtual FinanceOperationType FinanceOperationType { get; set; }
        public virtual Pocket Pocket { get; set; }
        public virtual ICollection<Auction> AuctionCommissionFinanceOperations { get; set; }
        public virtual ICollection<Auction> AuctionFinanceOperations { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }
    }
}
