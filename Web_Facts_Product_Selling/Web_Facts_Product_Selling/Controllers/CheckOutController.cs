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
using Microsoft.AspNetCore.Mvc;

namespace Web_Facts_Product_Selling.Controllers
{
	public class CheckOutController : Controller
	{
        private readonly Facts_ProductContext _context;

        public CheckOutController(Facts_ProductContext context)
        {
            _context = context;
        }

        public List<CartItem> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (data == null)
                {
                    data = new List<CartItem>();
                }
                return data;
            }
        }
        public IActionResult show_checkout()
		{
			return View();
		}

        public IActionResult successOrder()
        {
            return View();
        }

        public IActionResult payment()
        {
            return View();
        }
    }
}


