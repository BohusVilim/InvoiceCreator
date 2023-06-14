using InvoiceCreator.Models.EmailModels;
using InvoiceCreator.Services;
using InvoiceCreator.Services.Interfaces;

namespace KodimWeby.InternalServices
{
    public class EmailService
    {
        public static void ConfigureEmail(IServiceCollection services)
        {
            EmailServerConfiguration config = new EmailServerConfiguration
            {
                SmtpPassword = "kxpyriijdfiecdqf",
                SmtpServer = "smtp.gmail.com",
                SmtpUsername = "bohus.vilim@gmail.com"
            };

            EmailAddress FromEmailAddress = new EmailAddress
            {
                Address = "bohus.vilim@gmail.com",
                Name = "Invoice Creator"
            };

            services.AddSingleton<EmailServerConfiguration>(config);
            services.AddTransient<IEmailService, MailKitEmailService>();
            services.AddSingleton<EmailAddress>(FromEmailAddress);
        }
    }
}
