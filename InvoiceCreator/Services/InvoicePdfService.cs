using InvoiceCreator.Models.MainModels;
using InvoiceCreator.Models.ViewModels;

namespace InvoiceCreator.Services
{
    public class InvoicePdfService
    {
        private readonly InvoiceCreatorDbContext.InvoiceCreatorDbContext _context;
        public InvoicePdfService(InvoiceCreatorDbContext.InvoiceCreatorDbContext context)
        {
            _context = context;
        }

        public InvoicePdf CreateInvoicePdf() 
        {
            InvoicePdf invoicePdf = new InvoicePdf();

            invoicePdf.Invoice = _context.Invoices.FirstOrDefault(f => f.Id == _context.Invoices.Count());
            invoicePdf.Supplier = _context.Suppliers.FirstOrDefault(s => s.Id == invoicePdf.Invoice.SupplierId);
            invoicePdf.PaymentData = _context.PaymentDatas.FirstOrDefault(p => p.Id == invoicePdf.Supplier.PaymentDataId);
            invoicePdf.Costumer = _context.Costumers.FirstOrDefault(c => c.Id == invoicePdf.Invoice.CostumerId);
            invoicePdf.Services = _context.Services.Where(s => s.InvoiceId == invoicePdf.Invoice.Id).ToList();

            return invoicePdf;
        }
    }
}
