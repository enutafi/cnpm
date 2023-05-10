using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Web_Facts_Product_Selling.Models;

namespace Web_Facts_Product_Selling.Controllers
{
    public class HomeController : Controller
    {

        private readonly Facts_ProductContext _context;

        public HomeController(Facts_ProductContext context)
        {
            _context = context;
        }


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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
