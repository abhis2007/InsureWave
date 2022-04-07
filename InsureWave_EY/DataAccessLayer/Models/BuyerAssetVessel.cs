using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class BuyerAssetVessel
    {
        public int BuyerId { get; set; }
        public string UserId { get; set; }
        [Required]
        public int AssetId { get; set; }
        [Required]
        public int? CountryId { get; set; }
        [Required]
        public int? RequestStatus { get; set; }

        public virtual Asset Asset { get; set; }
        public virtual CountryCurrExchange Country { get; set; }
        public virtual Request RequestStatusNavigation { get; set; }
        public virtual User User { get; set; }
    }
}
