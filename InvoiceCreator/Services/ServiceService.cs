using InvoiceCreator.InvoiceCreatorDbContext;
using InvoiceCreator.Models.MainModels;

namespace InvoiceCreator.Services
{
    public class ServiceService
    {
        private readonly InvoiceCreatorDbContext.InvoiceCreatorDbContext _context;

        public ServiceService(InvoiceCreatorDbContext.InvoiceCreatorDbContext context)
        {
            _context = context;
        }

        public bool CreateService(Service service)
        {
            bool result = false;

            service.UnitPriceWithoutDPH = service.UnitPriceWithDPH / ((Convert.ToDecimal(service.DPH) + 100) / 100);
            service.TotalPriceWithDPH = service.UnitPriceWithDPH * service.NumberOfUnits;

            /*using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _inMemoryContext.Add(service);
                    _inMemoryContext.SaveChanges();
                    transaction.Commit();

                    result = true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }*/
            return result;
        }
    }
}
