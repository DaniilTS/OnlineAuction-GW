using System;
using System.Collections.Generic;

#nullable disable

namespace DBAL.Models
{
    public partial class FinanceOperationStatus
    {
        public FinanceOperationStatus()
        {
            FinanceOperations = new HashSet<FinanceOperation>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FinanceOperation> FinanceOperations { get; set; }
    }
}
