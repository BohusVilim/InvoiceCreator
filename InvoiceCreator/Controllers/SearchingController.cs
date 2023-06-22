using InvoiceCreator.InvoiceCreatorDbContext;
using InvoiceCreator.Models.MainModels;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceCreator.Controllers
{
    public class SearchingController : Controller
    {
        private readonly InvoiceCreatorInMemoryDbContext _inMemoryDbContext;
        public SearchingController(InvoiceCreatorInMemoryDbContext inMemoryDbContext) 
        { 
            _inMemoryDbContext = inMemoryDbContext;
        }
        [HttpPost]
        public IActionResult Search(Searching model)
        {
            var pastSearching = _inMemoryDbContext.Searchings;
            _inMemoryDbContext.Searchings.RemoveRange(pastSearching);
            _inMemoryDbContext.SaveChanges();

            _inMemoryDbContext.Searchings.Add(model);
            _inMemoryDbContext.SaveChanges();

            return RedirectToAction("Index", "Invoices");
        }
    }
}
