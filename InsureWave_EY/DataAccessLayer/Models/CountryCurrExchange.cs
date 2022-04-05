using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class CountryCurrExchange
    {
        public CountryCurrExchange()
        {
            BuyerAssetVessels = new HashSet<BuyerAssetVessel>();
        }

        public int CountryId { get; set; }
        public double Rate { get; set; }
        public string CountryName { get; set; }

        public virtual ICollection<BuyerAssetVessel> BuyerAssetVessels { get; set; }
    }
}
