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
    public class SuppliersController : Controller
    {
        private readonly Facts_ProductContext _context;

        public SuppliersController(Facts_ProductContext context)
        {
            _context = context;
        }

        // GET: Suppliers
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblSuppliers.ToListAsync());
        }

        // GET: Suppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblSupplier = await _context.TblSuppliers
                .FirstOrDefaultAsync(m => m.SupplierId == id);
            if (tblSupplier == null)
            {
                return NotFound();
            }

            return View(tblSupplier);
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplierId,SupplierName,SupplierEmail,SupplierUsername,SupplierPassword,SupplierPhone")] TblSupplier tblSupplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblSupplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblSupplier);
        }

        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblSupplier = await _context.TblSuppliers.FindAsync(id);
            if (tblSupplier == null)
            {
                return NotFound();
            }
            return View(tblSupplier);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupplierId,SupplierName,SupplierEmail,SupplierUsername,SupplierPassword,SupplierPhone")] TblSupplier tblSupplier)
        {
            if (id != tblSupplier.SupplierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblSupplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblSupplierExists(tblSupplier.SupplierId))
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
            return View(tblSupplier);
        }

        // GET: Suppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblSupplier = await _context.TblSuppliers
                .FirstOrDefaultAsync(m => m.SupplierId == id);
            if (tblSupplier == null)
            {
                return NotFound();
            }

            return View(tblSupplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblSupplier = await _context.TblSuppliers.FindAsync(id);
            _context.TblSuppliers.Remove(tblSupplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblSupplierExists(int id)
        {
            return _context.TblSuppliers.Any(e => e.SupplierId == id);
        }
    }
}
