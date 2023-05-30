using InvoiceCreator.Models.MainModels;

namespace InvoiceCreator.Models.ViewModels
{
    public class InvoicePdf
    {
        public Invoice? Invoice { get; set; } = null!;
        public Supplier? Supplier { get; set; }
        public PaymentData? PaymentData { get; set; }
        public Costumer? Costumer { get; set; }
        public IList<Service> Services { get; set; } = new List<Service>();

    }
}
