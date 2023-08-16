using InvoiceCreator.InvoiceCreatorDbContext;
using InvoiceCreator.Models.MainModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InvoiceCreator.Services
{
    public class InvoiceControllerService
    {
        private readonly InvoiceCreatorDbContext.InvoiceCreatorDbContext _context;
        public InvoiceControllerService(InvoiceCreatorDbContext.InvoiceCreatorDbContext context)
        {
            _context = context;
        }

        /*public List<Invoice>? Search()
        {
            var searchings = _context.Searchings.Where(a => a.SearchContent != null).ToList();

            if (searchings.Count() > 0)
            {
                var searchContent = searchings.FirstOrDefault().SearchContent;

                var list = _context.Invoices
                .Include(a => a.Supplier)
                .Include(a => a.Services)
                .Include(a => a.Costumer)
                .Where(b => b.NumberOfInvoice.ToString().Contains(searchContent)
                || b.DateOfIssue.ToString().Contains(searchContent)
                || b.TotalPrice.ToString().Contains(searchContent)
                || b.Note.Contains(searchContent)
                || b.Supplier.Name.Contains(searchContent)
                || b.Supplier.Address.Contains(searchContent)
                || b.Supplier.Country.Contains(searchContent)
                || b.Supplier.ICO.ToString().Contains(searchContent)
                || b.Supplier.DIC.ToString().Contains(searchContent)
                || b.Supplier.IC_DPH.Contains(searchContent)
                || b.Supplier.URL.Contains(searchContent)
                || b.Supplier.PaymentData.DueDate.ToString().Contains(searchContent)
                || b.Supplier.PaymentData.Bank.Contains(searchContent)
                || b.Supplier.PaymentData.IBAN.Contains(searchContent)
                || b.Supplier.PaymentData.SWIFT.Contains(searchContent)
                || b.Supplier.PaymentData.VariableSymbol.ToString().Contains(searchContent)
                || b.Supplier.PaymentData.ConstantSymbol.ToString().Contains(searchContent)
                || b.Supplier.PaymentData.Note.Contains(searchContent)
                || b.Costumer.Name.Contains(searchContent)
                || b.Costumer.Address.Contains(searchContent)
                || b.Costumer.Country.Contains(searchContent)
                || b.Costumer.ICO.ToString().Contains(searchContent)
                || b.Costumer.DIC.ToString().Contains(searchContent)
                || b.Costumer.IC_DPH.Contains(searchContent)
                || b.Costumer.URL.Contains(searchContent)
                || b.Services.Any(c => c.NameOfService.Contains(searchContent)
                    || c.DescriptionOfService.Contains(searchContent)
                    || c.UnitPriceWithDPH.ToString().Contains(searchContent)
                    || c.UnitPriceWithoutDPH.ToString().Contains(searchContent)
                    || c.TotalPriceWithDPH.ToString().Contains(searchContent)))
                .ToList();

                return list;
            }

            else
            {
                return null;
            }
            
        }*/
    }
}
