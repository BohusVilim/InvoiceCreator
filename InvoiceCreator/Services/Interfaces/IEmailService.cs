using InvoiceCreator.Models.EmailModels;

namespace InvoiceCreator.Services.Interfaces
{
    public interface IEmailService
    {
        void Send(EmailMessage message);
    }
}
