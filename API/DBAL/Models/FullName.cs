using System;
using System.Collections.Generic;

#nullable disable

namespace DBAL.Models
{
    public partial class FullName
    {
        public FullName()
        {
            Users = new HashSet<User>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
