using InvoiceCreator.Models.EmailModels;
using InvoiceCreator.Services.Interfaces;

namespace InvoiceCreator.Services
{
    public class EmailModelService
    {
        private EmailAddress _fromAndToEmailAddress;
        private IEmailService _emailService;
        public EmailModelService(EmailAddress emailAddress, IEmailService emailService)
        {
            _fromAndToEmailAddress = emailAddress;
            _emailService = emailService;
        }
        public void CreateAndSendEmail(EmailModel emailModel)
        {
            var fromAddress = new EmailAddress();
            fromAddress.Name = emailModel.SenderName;
            fromAddress.Address = _fromAndToEmailAddress.Address;

            var toAddress = new EmailAddress();
            toAddress.Name = emailModel.RecipientName;
            toAddress.Address = emailModel.RecipientEmail;

            EmailMessage msgToSend = new EmailMessage
            {
                FromAddresses = new List<EmailAddress> { fromAddress },
                ToAddresses = new List<EmailAddress> { toAddress },
                Content = $"Sender: {emailModel.SenderName}, " +
                    $"Email: {emailModel.SenderEmail}, Message: {emailModel.Message}",
                Subject = "Invoice created by Invoice Creator"
            };

            _emailService.Send(msgToSend);

            DeleteTemporaryInvoice();
        }

        public void DeleteTemporaryInvoice()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.pdf");
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
