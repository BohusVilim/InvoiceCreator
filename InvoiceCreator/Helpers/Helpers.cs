using InvoiceCreator.InvoiceCreatorDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceCreator.Helpers
{
    public class Helpers
    {
        private readonly InvoiceCreatorDbContext.InvoiceCreatorDbContext _context;
        private InvoiceCreatorInMemoryDbContext _inMemoryContext;

        public Helpers(InvoiceCreatorDbContext.InvoiceCreatorDbContext context, InvoiceCreatorInMemoryDbContext inMemoryContext)
        {
            _context = context;
            _inMemoryContext = inMemoryContext;
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

        public void ClearInMemoryDatabase()
        {
            var suppliers = _inMemoryContext.Suppliers.ToList();
            _inMemoryContext.Suppliers.RemoveRange(suppliers);

            var paymentDatas = _inMemoryContext.PaymentDatas.ToList();
            _inMemoryContext.PaymentDatas.RemoveRange(paymentDatas);

            var costumers = _inMemoryContext.Costumers.ToList();
            _inMemoryContext.Costumers.RemoveRange(costumers);

            var services = _inMemoryContext.Services.ToList();
            _inMemoryContext.Services.RemoveRange(services);

            _inMemoryContext.SaveChanges();
        }
    }
}
