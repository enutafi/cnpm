using System;
using System.Collections.Generic;

#nullable disable

namespace Web_Facts_Product_Selling.Models
{
    public partial class TblShipping
    {
        public int ShippingId { get; set; }
        public string ShippingName { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingPhone { get; set; }
        public string ShippingEmail { get; set; }
        public string ShippingNote { get; set; }
    }
}
