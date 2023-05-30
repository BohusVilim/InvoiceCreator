using Sprievodca.Models.Shared;

namespace InvoiceCreator.Models.MainModels
{
    public class PaymentData : BaseIdentity
    {
        public string Bank { get; set; } = null!;
        public string IBAN { get; set; } = null!;
        public string SWIFT { get; set; } = null!;
        public int VariableSymbol { get; set; }
        public int ConstantSymbol { get; set; }
        public string? Note { get; set; }
        public Supplier Supplier { get; set; }
    }
}
