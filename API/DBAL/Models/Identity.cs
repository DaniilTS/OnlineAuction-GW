using System;
using System.Collections.Generic;

#nullable disable

namespace DBAL.Models
{
    public partial class Identity
    {
        public Guid Id { get; set; }
        public Guid? IdentityTypeId { get; set; }
        public string Value { get; set; }
        public Guid UserId { get; set; }

        public virtual IdentityType IdentityType { get; set; }
        public virtual User User { get; set; }
    }
}
