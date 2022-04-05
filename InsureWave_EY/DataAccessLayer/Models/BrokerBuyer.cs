using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class BrokerBuyer
    {
        public int Bbid { get; set; }
        public string BrokerId { get; set; }
        public string UserId { get; set; }
        public int AssetId { get; set; }
        public int? PolicyStatus { get; set; }

        public virtual Broker Broker { get; set; }
        public virtual Request PolicyStatusNavigation { get; set; }
    }
}
