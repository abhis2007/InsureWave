using Microsoft.AspNetCore.Mvc.Rendering;
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
        public string CountryName { get; set; }
        public string PremiumType { get; set; }

        public List<SelectListItem> PremLists = new List<SelectListItem>() {
            new SelectListItem{ Text="Without depriciation",Value="Dep_0"},
            new SelectListItem{ Text="Depriciation of 10%",Value="Dep_10"},
            new SelectListItem{ Text="Depriciation of 30%",Value="Dep_30"},
            new SelectListItem{ Text="Depriciation of 50%",Value="Dep_50"}
        };

    }
}
