using System;
using System.Collections.Generic;

#nullable disable

namespace DBAL.Models
{
    public partial class LotImage
    {
        public Guid Id { get; set; }
        public Guid? LotId { get; set; }
        public string Url { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deleted { get; set; }

        public virtual Lot Lot { get; set; }
    }
}
