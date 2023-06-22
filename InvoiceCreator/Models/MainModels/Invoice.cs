using Sprievodca.Models.Shared;

namespace InvoiceCreator.Models.MainModels
{
    public class Invoice : BaseIdentity
    {
        public int NumberOfInvoice { get; set; }
        public DateTime DateOfIssue { get; set; }
        public Supplier Supplier { get; set; } = null!;
        public int SupplierId { get; set; }
        public Costumer Costumer { get; set; } = null!;
        public int CostumerId { get; set; }
        public IList<Service> Services { get; set; } = new List<Service>();
        public decimal TotalPrice { get; set; }
        public string? Note { get; set; }
    }
}
