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
    public class ShippingController : Controller
    {
        private readonly Facts_ProductContext _context;

        public ShippingController(Facts_ProductContext context)
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
        public IActionResult Save_Information_Shipping()
        {
            ViewData["CategoryId"] = new SelectList(_context.TblCategoryProducts, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> save_information_shipping([Bind("ShippingId,ShippingName,ShippingAddress,ShippingPhone,ShippingEmail,ShippingNote")] TblShipping tblShipping)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblShipping);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(controllerName: "CheckOutController", actionName: "payment");
            }
            return View(tblShipping);
        }

        
    }
}
