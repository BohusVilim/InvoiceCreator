using Sprievodca.Models.Shared;

namespace InvoiceCreator.Models.EmailModels
{
    public class EmailModel : BaseIdentity
    {
        public string RecipientName { get; set; }
        public string RecipientEmail { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Message { get; set; }
    }
}
