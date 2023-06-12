namespace InvoiceCreator.Models.EmailModels.Interfaces
{
    public interface IEmailService
    {
        void Send(EmailMessage message);
    }
}
