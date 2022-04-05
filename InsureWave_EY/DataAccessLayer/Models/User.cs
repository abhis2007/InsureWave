using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class User
    {
        public User()
        {
            Brokers = new HashSet<Broker>();
            BuyerAssetVessels = new HashSet<BuyerAssetVessel>();
            Insurers = new HashSet<Insurer>();
        }

        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GenderId { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Broker> Brokers { get; set; }
        public virtual ICollection<BuyerAssetVessel> BuyerAssetVessels { get; set; }
        public virtual ICollection<Insurer> Insurers { get; set; }
    }
}
