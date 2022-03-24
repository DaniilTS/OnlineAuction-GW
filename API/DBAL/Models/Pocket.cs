using System;
using System.Collections.Generic;

#nullable disable

namespace DBAL.Models
{
    public partial class Pocket
    {
        public Pocket()
        {
            FinanceOperations = new HashSet<FinanceOperation>();
        }

        public Guid Id { get; set; }
        public Guid HolderId { get; set; }
        public decimal Amount { get; set; }

        public virtual User Holder { get; set; }
        public virtual ICollection<FinanceOperation> FinanceOperations { get; set; }
    }
}
