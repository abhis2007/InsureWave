using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UILayer.Models
{
    public class MoreDetail
    {
        public string AssetName { get; set; }
        public string AssetType { get; set; }
        public string AssetInclusionDate { get; set; }
        public string  Broker { get; set; }
        public string Insurer { get; set; }
        public string CountryName { get; set; }
        public Dictionary<string,List<string>> status { get; set; }
    }
}
