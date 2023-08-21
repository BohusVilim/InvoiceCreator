using InvoiceCreator.InvoiceCreatorDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceCreator.Helpers
{
    public class Helpers
    {
        private readonly InvoiceCreatorDbContext.InvoiceCreatorDbContext _context;

        public Helpers(InvoiceCreatorDbContext.InvoiceCreatorDbContext context)
        {
            _context = context;
        }
        public int DefaultNumberOfInvoice()
        {
            var date = DateTime.Now;
            var year = date.Year.ToString();
            var nextNumbers = _context.Invoices.Count() + 1;
            var vsString = year + nextNumbers.ToString();
            var vs = int.Parse(vsString);

            return vs;
        }
    }
}
