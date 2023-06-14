using InvoiceCreator.Controllers;
using InvoiceCreator.Models.EmailModels;
using InvoiceCreator.Services.Interfaces;
using MimeKit;

namespace InvoiceCreator.Services
{
    public class MailKitEmailService : IEmailService
    {
        private readonly EmailServerConfiguration _eConfig;
        private readonly InvoicePatternController _invoicePatternController;

        public MailKitEmailService(EmailServerConfiguration config, InvoicePatternController invoicePatternController)
        {
            _eConfig = config;
            _invoicePatternController = invoicePatternController;
        }

        public void Send(EmailMessage msg)
        {
            var message = new MimeMessage();
            message.To.AddRange(msg.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
            message.From.AddRange(msg.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

            message.Subject = msg.Subject;

            var builder = new BodyBuilder();
            builder.TextBody = msg.Content;

            _invoicePatternController.CreateTemporaryPdf();

            builder.Attachments.Add("Invoice.pdf");

            message.Body = builder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(_eConfig.SmtpServer, _eConfig.SmtpPort);

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(_eConfig.SmtpUsername, _eConfig.SmtpPassword);

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
