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
    public class CostumersController : Controller
    {
        private readonly InvoiceCreatorDbContext.InvoiceCreatorDbContext _context;
        private readonly InvoiceCreatorInMemoryDbContext _inMemoryContext;

        public CostumersController(InvoiceCreatorDbContext.InvoiceCreatorDbContext context, InvoiceCreatorInMemoryDbContext inMemoryContext)
        {
            _context = context;
            _inMemoryContext = inMemoryContext;
        }

        // GET: Costumers
        public async Task<IActionResult> Index()
        {
              return _context.Costumers != null ? 
                          View(await _context.Costumers.ToListAsync()) :
                          Problem("Entity set 'InvoiceCreatorDbContext.Costumers'  is null.");
        }

        // GET: Costumers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Costumers == null)
            {
                return NotFound();
            }

            var costumer = await _context.Costumers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (costumer == null)
            {
                return NotFound();
            }

            return View(costumer);
        }

        // GET: Costumers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Costumers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Costumer costumer)
        {
            ModelState.Remove("Invoice");
            if (ModelState.IsValid)
            {
                _inMemoryContext.Add(costumer);
                _inMemoryContext.SaveChanges();
                return RedirectToAction("Create", "Services");
            }
            return View(costumer);
        }

        // GET: Costumers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Costumers == null)
            {
                return NotFound();
            }

            var costumer = await _context.Costumers.FindAsync(id);
            if (costumer == null)
            {
                return NotFound();
            }
            return View(costumer);
        }

        // POST: Costumers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,Address,Country,ICO,DIC,IsDPHPayer,IC_DPH,URL,Id")] Costumer costumer)
        {
            if (id != costumer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(costumer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CostumerExists(costumer.Id))
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
            return View(costumer);
        }

        // GET: Costumers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Costumers == null)
            {
                return NotFound();
            }

            var costumer = await _context.Costumers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (costumer == null)
            {
                return NotFound();
            }

            return View(costumer);
        }

        // POST: Costumers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Costumers == null)
            {
                return Problem("Entity set 'InvoiceCreatorDbContext.Costumers'  is null.");
            }
            var costumer = await _context.Costumers.FindAsync(id);
            if (costumer != null)
            {
                _context.Costumers.Remove(costumer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CostumerExists(long id)
        {
          return (_context.Costumers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
