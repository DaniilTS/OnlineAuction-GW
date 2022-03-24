﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DBAL.Models
{
    public partial class Auction
    {
        public Guid Id { get; set; }
        public Guid AuctionTypeId { get; set; }
        public Guid LotId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Guid? WinnerId { get; set; }
        public decimal? EndPrice { get; set; }
        public Guid? FinanceOperationId { get; set; }
        public bool IsEmailMessageSended { get; set; }
        public DateTime Created { get; set; }

        public virtual AuctionType AuctionType { get; set; }
        public virtual FinanceOperation FinanceOperation { get; set; }
        public virtual Lot Lot { get; set; }
        public virtual User Winner { get; set; }
    }
}