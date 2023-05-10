using System;
using System.Collections.Generic;

#nullable disable

namespace Web_Facts_Product_Selling.Models
{
    public partial class TblSupplier
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierEmail { get; set; }
        public string SupplierUsername { get; set; }
        public string SupplierPassword { get; set; }
        public string SupplierPhone { get; set; }
    }
}
