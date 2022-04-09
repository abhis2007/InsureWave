using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class PolicyDetail
    {
        public PolicyDetail()
        {
            PremiumAmountDetails = new HashSet<PremiumAmountDetail>();
        }

        public int Pid { get; set; }
        public string BuyerId { get; set; }
        public int? Ibid { get; set; }
        public string PolicyStatus { get; set; }
        public int AssetId { get; set; }

        public virtual InsurerBroker Ib { get; set; }
        public virtual ICollection<PremiumAmountDetail> PremiumAmountDetails { get; set; }
    }
}
