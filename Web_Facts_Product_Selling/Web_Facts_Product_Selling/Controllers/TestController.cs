using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Web_Facts_Product_Selling.Models;

namespace Web_Facts_Product_Selling.Controllers
{
    public class TestController : Controller
    {
        private readonly Facts_ProductContext _context;

        public TestController(Facts_ProductContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveInformation([Bind("AdminId,AdminEmail,AdminPassword,AdminName,AdminPhone")] TblAdmin tblAdmin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblAdmin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblAdmin);
        }
    }
}
