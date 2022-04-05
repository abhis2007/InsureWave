using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class BuyerAssetPolicy
    {
        public int BuyerAssetId { get; set; }
        public int? AssetId { get; set; }
        public int PolicyId { get; set; }

        public virtual Asset Asset { get; set; }
    }
}
