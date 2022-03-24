using System;
using System.Collections.Generic;

#nullable disable

namespace DBAL.Models
{
    public partial class BalanceOperationType
    {
        public BalanceOperationType()
        {
            FinanceOperationTypes = new HashSet<FinanceOperationType>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsPositive { get; set; }

        public virtual ICollection<FinanceOperationType> FinanceOperationTypes { get; set; }
    }
}
