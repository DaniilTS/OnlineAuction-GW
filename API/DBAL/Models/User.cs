using System;
using System.Collections.Generic;

#nullable disable

namespace DBAL.Models
{
    public partial class User
    {
        public User()
        {
            Auctions = new HashSet<Auction>();
            Lots = new HashSet<Lot>();
            Offers = new HashSet<Offer>();
            Pockets = new HashSet<Pocket>();
            UserImages = new HashSet<UserImage>();
        }

        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid? GenderId { get; set; }
        public Guid? FullNameId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string PhotoUrl { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual FullName FullName { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Auction> Auctions { get; set; }
        public virtual ICollection<Lot> Lots { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }
        public virtual ICollection<Pocket> Pockets { get; set; }
        public virtual ICollection<UserImage> UserImages { get; set; }
    }
}
