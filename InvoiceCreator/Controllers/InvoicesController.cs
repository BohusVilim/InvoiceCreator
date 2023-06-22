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
using InvoiceCreator.MockingGenerator;
using System.Text.Json;

namespace InvoiceCreator.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly InvoiceCreatorDbContext.InvoiceCreatorDbContext _context;
        private readonly InvoiceCreatorInMemoryDbContext _inMemoryContext;
        private readonly InvoiceService _invoiceService;
        private readonly Helpers.Helpers _helpers;
        private readonly MockingData _mockingData;
        private readonly InvoiceControllerService _invoiceControllerService;

        public InvoicesController(InvoiceCreatorDbContext.InvoiceCreatorDbContext context, 
            InvoiceCreatorInMemoryDbContext inMemoryContext, 
            InvoiceService invoiceService, 
            Helpers.Helpers helpers, 
            MockingData mockingData,
            InvoiceControllerService invoiceControllerService)
        {
            _context = context;
            _inMemoryContext = inMemoryContext;
            _invoiceService = invoiceService;
            _helpers = helpers;
            _mockingData = mockingData;
            _invoiceControllerService = invoiceControllerService;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            ViewBag.Search = _invoiceControllerService.Search();

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
            _mockingData.FillInMemoryDatabase();

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
                return Json(new { success = true });
            }

            return View(invoice);
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
