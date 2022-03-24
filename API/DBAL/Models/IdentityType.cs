using System;
using System.Collections.Generic;

#nullable disable

namespace DBAL.Models
{
    public partial class IdentityType
    {
        public IdentityType()
        {
            Identities = new HashSet<Identity>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Identity> Identities { get; set; }
    }
}
