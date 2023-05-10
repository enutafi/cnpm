using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Facts_Product_Selling.ViewModel
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductImage { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int NumberOfItems { get; set; }
        public double subtotal => NumberOfItems * ProductPrice;
        public double Tax => NumberOfItems * ProductPrice / 10;

    }
}
