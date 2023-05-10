using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_Facts_Product_Selling.Models;

namespace Web_Facts_Product_Selling.Controllers
{
    public class CategoryProductsController : Controller
    {
        private readonly Facts_ProductContext _context;

        public CategoryProductsController(Facts_ProductContext context)
        {
            _context = context;
        }

        // GET: CategoryProducts
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblCategoryProducts.ToListAsync());
        }

        // GET: CategoryProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCategoryProduct = await _context.TblCategoryProducts
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (tblCategoryProduct == null)
            {
                return NotFound();
            }

            return View(tblCategoryProduct);
        }

        // GET: CategoryProducts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoryProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryDesc,CategoryStatus")] TblCategoryProduct tblCategoryProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblCategoryProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblCategoryProduct);
        }

        // GET: CategoryProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCategoryProduct = await _context.TblCategoryProducts.FindAsync(id);
            if (tblCategoryProduct == null)
            {
                return NotFound();
            }
            return View(tblCategoryProduct);
        }

        // POST: CategoryProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,CategoryDesc,CategoryStatus")] TblCategoryProduct tblCategoryProduct)
        {
            if (id != tblCategoryProduct.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblCategoryProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblCategoryProductExists(tblCategoryProduct.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tblCategoryProduct);
        }

        // GET: CategoryProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCategoryProduct = await _context.TblCategoryProducts
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (tblCategoryProduct == null)
            {
                return NotFound();
            }

            return View(tblCategoryProduct);
        }

        // POST: CategoryProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblCategoryProduct = await _context.TblCategoryProducts.FindAsync(id);
            _context.TblCategoryProducts.Remove(tblCategoryProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblCategoryProductExists(int id)
        {
            return _context.TblCategoryProducts.Any(e => e.CategoryId == id);
        }
    }
}
