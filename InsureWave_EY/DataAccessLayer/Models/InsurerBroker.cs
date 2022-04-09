using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class InsurerBroker
    {
        public InsurerBroker()
        {
            PolicyDetails = new HashSet<PolicyDetail>();
        }

        public int Ibid { get; set; }
        public string BrokerId { get; set; }
        public string InsurerId { get; set; }
        public string BuyerId { get; set; }
        public int InsuranceTenure { get; set; }
        public int? AssetId { get; set; }
        public int? BrokerageCharge { get; set; }

        public virtual Broker Broker { get; set; }
        public virtual Insurer Insurer { get; set; }
        public virtual ICollection<PolicyDetail> PolicyDetails { get; set; }
    }
}
