using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class Broker
    {
        public Broker()
        {
            BrokerBuyers = new HashSet<BrokerBuyer>();
            InsurerBrokers = new HashSet<InsurerBroker>();
        }

        public string BrokerId { get; set; }
        public string UserId { get; set; }
        public int LicenseId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<BrokerBuyer> BrokerBuyers { get; set; }
        public virtual ICollection<InsurerBroker> InsurerBrokers { get; set; }
    }
}
