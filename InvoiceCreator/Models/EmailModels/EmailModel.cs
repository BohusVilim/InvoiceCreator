using Sprievodca.Models.Shared;

namespace InvoiceCreator.Models.EmailModels
{
    public class EmailModel : BaseIdentity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
