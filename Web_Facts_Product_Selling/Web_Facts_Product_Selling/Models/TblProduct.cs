using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace Web_Facts_Product_Selling.Models
{
    public partial class TblProduct
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public string ProductContent { get; set; }
        public string ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public bool ProductStatus { get; set; }

        public virtual TblCategoryProduct Category { get; set; }
    }
}
