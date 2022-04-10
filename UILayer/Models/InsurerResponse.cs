using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UILayer.Models
{
    public class InsurerResponse
    {
        public string AssetName { get; set; }
        public string AssetType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        [Required(ErrorMessage ="Interval of Emi")]
        public int EmiInterval { get; set; }
        public string BrokerName { get; set; }
        public string BuyerName { get; set; }
        [Required(ErrorMessage ="Feedback are welcome always")]
        public string Feedback { get; set; }
        public string LastInsurerName { get; set; }
        public string BuyerId { get; set; }
        public int AssetId { get; set; }
        [Required(ErrorMessage ="Premium amount is blank")]
        public int PremiumAmount { get; set; }
        [Required(ErrorMessage ="DownPay cant be blank")]
        public int DownPay { get; set; }

    }
}
