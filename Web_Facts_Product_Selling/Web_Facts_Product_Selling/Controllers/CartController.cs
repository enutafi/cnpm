using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_Facts_Product_Selling.Models;
using Web_Facts_Product_Selling.Helpers;
using Web_Facts_Product_Selling.ViewModel;

namespace Web_Facts_Product_Selling.Controllers
{
    public class CartController : Controller
    {
        private readonly Facts_ProductContext _context;

        public CartController(Facts_ProductContext context)
        {
            _context = context;
        }

           public List<CartItem> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if(data == null)
                {
                    data = new List<CartItem>();
                }
                return data;    
            }
        }

        [HttpPost]
        public IActionResult AddToCart(int? id, int numberOfItems)
        {
            var myCart = Carts;
            var item = myCart.SingleOrDefault(p => p.ProductId == id);

            if(item == null)
            {
                var item_info = _context.TblProducts.SingleOrDefault(p => p.ProductId == id);
                item = new CartItem
                {
                    ProductId = item_info.ProductId,
                    ProductImage = item_info.ProductImage,
                    ProductPrice = double.Parse(item_info.ProductPrice),
                   NumberOfItems = numberOfItems
                };
                myCart.Add(item);
            }
            else
            {
                item.NumberOfItems = item.NumberOfItems + numberOfItems;
            }
            HttpContext.Session.Set("GioHang", myCart);
            return RedirectToAction("show_cart");
        }

        public IActionResult show_cart()
        {   
            return View(Carts);
        }

        public IActionResult show_items_in_payment()
        {
            return View(Carts);
        }

    }
}
