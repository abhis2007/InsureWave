using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage ="Seems you forgot to choose your unique userid")]
        public string UserId { get; set; }
        [Required(ErrorMessage ="First name please")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Last name please")]
        public string LastName { get; set; }
        [Required(ErrorMessage ="Your gender please")]
        public string GenderId { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage ="Who want you to became?")]
        public int? RoleId { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Broker> Brokers { get; set; }
        public virtual ICollection<BuyerAssetVessel> BuyerAssetVessels { get; set; }
        public virtual ICollection<Insurer> Insurers { get; set; }
    }
}
