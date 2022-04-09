using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class PremiumAmountDetail
    {
        public int PremAmtId { get; set; }
        public int? Pid { get; set; }
        public int IntervalOfEmi { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public float DownPay { get; set; }
        public float PremAmt { get; set; }
        public int Tenure { get; set; }
        public string PreType { get; set; }

        public virtual PolicyDetail PidNavigation { get; set; }
    }
}
