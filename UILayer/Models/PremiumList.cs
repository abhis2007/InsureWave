using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UILayer.Models
{
    public class PremiumList
    {
        public string PremList { get; set; }

        public List<SelectListItem> PremLists = new List<SelectListItem>() { 
            new SelectListItem{ Text="Without depriciation",Value="Dep_0"},
            new SelectListItem{ Text="Depriciation of 10%",Value="Dep_10"},
            new SelectListItem{ Text="Depriciation of 30%",Value="Dep_30"},
            new SelectListItem{ Text="Depriciation of 50%",Value="Dep_50"}
        };

    }
}
