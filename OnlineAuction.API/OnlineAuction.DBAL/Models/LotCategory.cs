using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineAuction.DBAL.Models
{
    public partial class LotCategory
    {
        public LotCategory()
        {
            Lots = new HashSet<Lot>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Lot> Lots { get; set; }
    }
}
