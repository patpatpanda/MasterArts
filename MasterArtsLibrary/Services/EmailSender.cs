using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Services
{
    public class EmailSender : IEmailSender, IOrderEmailSender
    {
        public string SendGridSecret { get; set; }
        public EmailSender(IConfiguration _config)
        {
            SendGridSecret = _config.GetValue<string>("SendGrid:SecretKey");
        }
          public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SendGridClient(SendGridSecret);
            var from = new EmailAddress("emil.arrenius@student.kyh.se", "Kund");
            var to = new EmailAddress("nils-emil1337@hotmail.se");

            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);

            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new InvalidOperationException($"Failed to send email. StatusCode: {response.StatusCode}");
            }
        }
        public async Task SendEmailOrderAsync(string recipientEmail, string subject, string htmlMessage)
        {
            var client = new SendGridClient(SendGridSecret);
            var from = new EmailAddress("emil.arrenius@student.kyh.se", "ARTS");
            var to = new EmailAddress(recipientEmail);  // Använd den angivna mottagarens e-postadress

            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);

            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new InvalidOperationException($"Failed to send email. StatusCode: {response.StatusCode}");
            }
        }

    }
}
