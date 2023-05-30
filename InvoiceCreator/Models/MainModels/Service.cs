using Sprievodca.Models.Shared;

namespace InvoiceCreator.Models.MainModels
{
    public class Service : BaseIdentity
    {
        public string NameOfService { get; set; } = null!;
        public string DescriptionOfService { get; set; } = null!;
        public int DPH { get; set; }
        public decimal UnitPriceWithDPH { get; set; }
        public int NumberOfUnits { get; set; }
        public decimal UnitPriceWithoutDPH { get; set; }
        public decimal TotalPriceWithDPH { get; set; }

        public Invoice Invoice { get; set; } = null!;
        public int InvoiceId { get; set; }
    }
}
