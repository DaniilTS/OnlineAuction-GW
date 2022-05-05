using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineAuction.DBAL.Models
{
    public partial class FinanceOperationType
    {
        public FinanceOperationType()
        {
            FinanceOperations = new HashSet<FinanceOperation>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid BalanceOperationTypeId { get; set; }

        public virtual BalanceOperationType BalanceOperationType { get; set; }
        public virtual ICollection<FinanceOperation> FinanceOperations { get; set; }
    }
}
