using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class Feedback
    {
        public int Fid { get; set; }
        public string UserId { get; set; }
        public int AssetId { get; set; }
        public string Response { get; set; }
        public string BuyerId { get; set; }
    }
}
