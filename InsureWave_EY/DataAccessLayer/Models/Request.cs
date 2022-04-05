using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class Request
    {
        public Request()
        {
            BrokerBuyers = new HashSet<BrokerBuyer>();
            BuyerAssetVessels = new HashSet<BuyerAssetVessel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BrokerBuyer> BrokerBuyers { get; set; }
        public virtual ICollection<BuyerAssetVessel> BuyerAssetVessels { get; set; }
    }
}
