using System;
using System.Collections.Generic;

#nullable disable

namespace Web_Facts_Product_Selling.Models
{
    public partial class TblAdmin
    {
        public int AdminId { get; set; }
        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; }
        public string AdminName { get; set; }
        public string AdminPhone { get; set; }
    }
}
