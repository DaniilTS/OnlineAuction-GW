using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineAuction.DBAL.Models
{
    public partial class Offer
    {
        public Guid Id { get; set; }
        public Guid CreatorId { get; set; }
        public Guid LotId { get; set; }
        public decimal Amount { get; set; }
        public decimal Commission { get; set; }
        public Guid OfferStatusId { get; set; }
        public Guid? FinanceOperationId { get; set; }
        public Guid? CommissionFinanceOperationId { get; set; }
        public DateTime Created { get; set; }

        public virtual FinanceOperation CommissionFinanceOperation { get; set; }
        public virtual User Creator { get; set; }
        public virtual FinanceOperation FinanceOperation { get; set; }
        public virtual Lot Lot { get; set; }
        public virtual OfferStatus OfferStatus { get; set; }
    }
}
