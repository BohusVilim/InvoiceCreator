namespace InvoiceCreator.Models.MainModels
{
    public class Supplier : CompanyBaseModel
    {
        public PaymentData PaymentData { get; set; } = null!;
        public int PaymentDataId { get; set; }
    }
}
 