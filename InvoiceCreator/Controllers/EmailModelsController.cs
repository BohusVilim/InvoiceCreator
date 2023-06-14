using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InvoiceCreator.InvoiceCreatorDbContext;
using InvoiceCreator.Models.EmailModels;
using KodimWeby.InternalServices;
using InvoiceCreator.Services.Interfaces;
using InvoiceCreator.Services;

namespace InvoiceCreator.Controllers
{
    public class EmailModelsController : Controller
    {
        private readonly InvoiceCreatorDbContext.InvoiceCreatorDbContext _context;
        private EmailAddress _fromAndToEmailAddress;
        private IEmailService _emailService;
        private EmailModelService _emailModelService;

        public EmailModelsController(InvoiceCreatorDbContext.InvoiceCreatorDbContext context, EmailAddress emailAddress, IEmailService emailService, EmailModelService emailModelService)
        {
            _context = context;
            _fromAndToEmailAddress = emailAddress;
            _emailService = emailService;
            _emailModelService = emailModelService;
        }

        // GET: EmailModels
        public async Task<IActionResult> Index()
        {
              return _context.EmailModel != null ? 
                          View(await _context.EmailModel.ToListAsync()) :
                          Problem("Entity set 'InvoiceCreatorDbContext.EmailModel'  is null.");
        }

        // GET: EmailModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmailModel == null)
            {
                return NotFound();
            }

            var emailModel = await _context.EmailModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emailModel == null)
            {
                return NotFound();
            }

            return View(emailModel);
        }

        // GET: EmailModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmailModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmailModel emailModel)
        {
            if (ModelState.IsValid)
            {
                _emailModelService.CreateAndSendEmail(emailModel);
                
                return RedirectToAction("Index", "Home"); 
            }
            return View(emailModel);
        }

        // GET: EmailModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmailModel == null)
            {
                return NotFound();
            }

            var emailModel = await _context.EmailModel.FindAsync(id);
            if (emailModel == null)
            {
                return NotFound();
            }
            return View(emailModel);
        }

        // POST: EmailModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Email,Message,Id")] EmailModel emailModel)
        {
            if (id != emailModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emailModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailModelExists(emailModel.Id))
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
            return View(emailModel);
        }

        // GET: EmailModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmailModel == null)
            {
                return NotFound();
            }

            var emailModel = await _context.EmailModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emailModel == null)
            {
                return NotFound();
            }

            return View(emailModel);
        }

        // POST: EmailModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EmailModel == null)
            {
                return Problem("Entity set 'InvoiceCreatorDbContext.EmailModel'  is null.");
            }
            var emailModel = await _context.EmailModel.FindAsync(id);
            if (emailModel != null)
            {
                _context.EmailModel.Remove(emailModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmailModelExists(int id)
        {
          return (_context.EmailModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
