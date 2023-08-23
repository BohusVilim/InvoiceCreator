using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InvoiceCreator.InvoiceCreatorDbContext;
using InvoiceCreator.Models.MainModels;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using InvoiceCreator.Services;
using System.Text.Json;

namespace InvoiceCreator.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly InvoiceCreatorDbContext.InvoiceCreatorDbContext _context;
        private readonly Helpers.Helpers _helpers;
        private readonly SearchingService _searchingService;

        public InvoicesController(InvoiceCreatorDbContext.InvoiceCreatorDbContext context, 
            Helpers.Helpers helpers, 
            SearchingService searchingService
            )
        {
            _context = context;
            _helpers = helpers;
            _searchingService = searchingService;
        }

        // GET: Invoices
        public async Task<IActionResult> Index(Searching searching)
        {
            if (searching.SearchContent != null)
            {
                ViewBag.Search = _searchingService.Search(searching);
            }

            return _context.Invoices != null ? 
                          View(await _context.Invoices
                          .Include(a => a.Costumer)
                          .Include(b => b.Supplier)
                            .ThenInclude(s => s.PaymentData)
                          .Include(c => c.Services)
                          .ToListAsync()) :
                          Problem("Entity set 'InvoiceCreatorDbContext.Invoices'  is null.");
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            ViewBag.NumberOfInvoice = _helpers.DefaultNumberOfInvoice();

            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Invoice invoice)
        {
            foreach (var service in invoice.Services)
            {
                service.UnitPriceWithoutDPH = service.UnitPriceWithDPH / (service.DPH / 100 + 1);
                service.TotalPriceWithDPH = service.UnitPriceWithoutDPH * service.NumberOfUnits;
            }

            _context.Invoices.Add(invoice);
            _context.SaveChanges();

            return Json(new { success = true });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoices = await _context.Invoices
                .Include(a => a.Costumer)
                .Include(b => b.Supplier)
                  .ThenInclude(s => s.PaymentData)
                .Include(c => c.Services)
                .ToListAsync();
            var invoice = invoices.FirstOrDefault(a => a.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(invoice.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Invoices == null)
            {
                return Problem("Entity set 'InvoiceCreatorDbContext.Invoices'  is null.");
            }
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(long id)
        {
          return (_context.Invoices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
