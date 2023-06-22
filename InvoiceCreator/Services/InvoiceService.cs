using InvoiceCreator.InvoiceCreatorDbContext;
using InvoiceCreator.Models.MainModels;

namespace InvoiceCreator.Services
{
    public class InvoiceService
    {
        private readonly InvoiceCreatorDbContext.InvoiceCreatorDbContext _context;
        private readonly InvoiceCreatorInMemoryDbContext _inMemoryContext;
        public InvoiceService(InvoiceCreatorDbContext.InvoiceCreatorDbContext context, InvoiceCreatorInMemoryDbContext inMemoryContext)
        {
            _context = context;
            _inMemoryContext = inMemoryContext;
        }

        public bool CreateInvoice(Invoice invoice)
        {
            bool result = false;

            var supplier = _inMemoryContext.Suppliers.LastOrDefault();
            var paymentData = _inMemoryContext.PaymentDatas.LastOrDefault();
            var costumer = _inMemoryContext.Costumers.LastOrDefault();
            var services = _inMemoryContext.Services.ToList();

            supplier.Id = 0;
            paymentData.Id = 0;
            costumer.Id = 0;

            var listServices = new List<Service>();
            decimal totalPrice = 0;
            foreach (var service in services)
            {
                totalPrice = totalPrice + service.TotalPriceWithDPH;
                service.Id = 0;
                listServices.Add(service);
            }

            supplier.PaymentData = paymentData;
            invoice.Supplier = supplier;
            invoice.Costumer = costumer;
            invoice.Services = listServices;
            invoice.TotalPrice = totalPrice;
            supplier.Invoice = invoice;
            costumer.Invoice = invoice;

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.PaymentDatas.Add(paymentData);
                    _context.Suppliers.Add(supplier);
                    _context.Costumers.Add(costumer);
                    _context.Services.AddRange(listServices);
                    _context.Invoices.Add(invoice);

                    _context.SaveChanges();
                    transaction.Commit();

                    result = true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return result;
        }

        public bool UpdateInvoice(int id, Invoice invoice)
        {
            bool result = false;



            return result;
        }
    }
}
