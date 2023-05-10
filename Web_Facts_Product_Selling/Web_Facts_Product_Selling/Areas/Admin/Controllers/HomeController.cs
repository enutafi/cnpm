using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using Web_Facts_Product_Selling.Models;

using Microsoft.AspNetCore.Mvc;
//using PagedList.Core;

namespace Facts_Product.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {   

        private readonly Facts_ProductContext _context;

        public HomeController(Facts_ProductContext context)
        {
            _context = context;
        }

        [Area("Admin")]
        
        /*
        public IActionResult Index()
        {
            var lsAdmin = _context.TblAdmins.OrderByDescending(x => x.AdminId);
            //PagedList<TblAdmin> models = new PagedList<TblAdmin>(lsAdmin);
            return View(lsAdmin);
        }
        */

        public async Task<IActionResult> Index()
        {
            //var lsAdmin = _context.TblAdmins.OrderByDescending(x => x.AdminId);
            //PagedList<TblAdmin> models = new PagedList<TblAdmin>(lsAdmin);
            return View(await _context.TblProducts.ToListAsync());
        }




        [Area("Admin")]
        public IActionResult Test()
        {
            return View();
        }
    }
}
