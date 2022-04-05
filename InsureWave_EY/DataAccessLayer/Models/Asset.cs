using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class Asset
    {
        public Asset()
        {
            BuyerAssetPolicies = new HashSet<BuyerAssetPolicy>();
            BuyerAssetVessels = new HashSet<BuyerAssetVessel>();
        }

        public int AssetId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public virtual ICollection<BuyerAssetPolicy> BuyerAssetPolicies { get; set; }
        public virtual ICollection<BuyerAssetVessel> BuyerAssetVessels { get; set; }
    }
}
