using Sprievodca.Models.Shared;

namespace InvoiceCreator.Models.MainModels
{
    public class CompanyBaseModel : BaseIdentity
    {
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Country { get; set; } = null!;
        public int ICO { get; set; }
        public int DIC { get; set; }
        public bool IsDPHPayer { get; set; }
        public string? IC_DPH { get; set; }
        public string? URL { get; set; }
        public Invoice Invoice { get; set; }
    }
}
