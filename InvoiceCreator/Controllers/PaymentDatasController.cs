using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InvoiceCreator.InvoiceCreatorDbContext;
using InvoiceCreator.Models.MainModels;

namespace InvoiceCreator.Controllers
{
    public class PaymentDatasController : Controller
    {
        private readonly InvoiceCreatorDbContext.InvoiceCreatorDbContext _context;
        private readonly InvoiceCreatorInMemoryDbContext _inMemoryContext;
        private readonly Helpers.Helpers _helpers;

        public PaymentDatasController(InvoiceCreatorDbContext.InvoiceCreatorDbContext context, InvoiceCreatorInMemoryDbContext inMemoryContext, Helpers.Helpers helpers)
        {
            _context = context;
            _inMemoryContext = inMemoryContext;
            _helpers = helpers;
        }

        // GET: PaymentDatas
        public async Task<IActionResult> Index()
        {
              return _context.PaymentDatas != null ? 
                          View(await _context.PaymentDatas.ToListAsync()) :
                          Problem("Entity set 'InvoiceCreatorDbContext.PaymentDatas'  is null.");
        }

        // GET: PaymentDatas/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.PaymentDatas == null)
            {
                return NotFound();
            }

            var paymentData = await _context.PaymentDatas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentData == null)
            {
                return NotFound();
            }

            return View(paymentData);
        }

        // GET: PaymentDatas/Create
        public IActionResult Create()
        {
            ViewBag.Vs = _helpers.DefaultNumberOfInvoice();

            return View();
        }

        // POST: PaymentDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PaymentData paymentData)
        {
            ModelState.Remove("Supplier");
            if (ModelState.IsValid)
            {
                _inMemoryContext.Add(paymentData);
                _inMemoryContext.SaveChanges();
                return RedirectToAction("Create", "Costumers");
            }
            return View(paymentData);
        }

        // GET: PaymentDatas/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.PaymentDatas == null)
            {
                return NotFound();
            }

            var paymentData = await _context.PaymentDatas.FindAsync(id);
            if (paymentData == null)
            {
                return NotFound();
            }
            return View(paymentData);
        }

        // POST: PaymentDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Bank,IBAN,SWIFT,VariableSymbol,ConstantSymbol,Note,Id")] PaymentData paymentData)
        {
            if (id != paymentData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentDataExists(paymentData.Id))
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
            return View(paymentData);
        }

        // GET: PaymentDatas/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.PaymentDatas == null)
            {
                return NotFound();
            }

            var paymentData = await _context.PaymentDatas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentData == null)
            {
                return NotFound();
            }

            return View(paymentData);
        }

        // POST: PaymentDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.PaymentDatas == null)
            {
                return Problem("Entity set 'InvoiceCreatorDbContext.PaymentDatas'  is null.");
            }
            var paymentData = await _context.PaymentDatas.FindAsync(id);
            if (paymentData != null)
            {
                _context.PaymentDatas.Remove(paymentData);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentDataExists(long id)
        {
          return (_context.PaymentDatas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
