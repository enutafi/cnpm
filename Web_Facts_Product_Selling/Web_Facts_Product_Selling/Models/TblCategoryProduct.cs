using System;
using System.Collections.Generic;

#nullable disable

namespace Web_Facts_Product_Selling.Models
{
    public partial class TblCategoryProduct
    {
        public TblCategoryProduct()
        {
            TblProducts = new HashSet<TblProduct>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDesc { get; set; }
        public bool CategoryStatus { get; set; }

        public virtual ICollection<TblProduct> TblProducts { get; set; }
    }
}
