using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class Insurer
    {
        public Insurer()
        {
            InsurerBrokers = new HashSet<InsurerBroker>();
        }

        public string InsurerId { get; set; }
        public string UserId { get; set; }
        public int LicenseId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<InsurerBroker> InsurerBrokers { get; set; }
    }
}
