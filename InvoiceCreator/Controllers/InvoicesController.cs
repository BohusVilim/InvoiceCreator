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

namespace InvoiceCreator.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly InvoiceCreatorDbContext.InvoiceCreatorDbContext _context;
        private readonly InvoiceCreatorInMemoryDbContext _inMemoryContext;
        private readonly InvoiceService _invoiceService;
        private readonly Helpers.Helpers _helpers;

        public InvoicesController(InvoiceCreatorDbContext.InvoiceCreatorDbContext context, InvoiceCreatorInMemoryDbContext inMemoryContext, InvoiceService invoiceService, Helpers.Helpers helpers)
        {
            _context = context;
            _inMemoryContext = inMemoryContext;
            _invoiceService = invoiceService;
            _helpers = helpers;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
              return _context.Invoices != null ? 
                          View(await _context.Invoices.ToListAsync()) :
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

            if (_invoiceService.CreateInvoice(invoice) == true)
            {
                _helpers.ClearInMemoryDatabase();
                return RedirectToAction("Index", "InvoicePattern");
            }

            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("NumberOfInvoice,TotalPrice,Note,Id")] Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            return View(invoice);
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
