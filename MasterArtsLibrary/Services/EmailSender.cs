﻿using Microsoft.AspNetCore.Identity.UI.Services;
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
        private ISendGridClient _sendGridClient;

        public EmailSender(IConfiguration config, ISendGridClient sendGridClient = null)
        {
            SendGridSecret = config["SendGrid:SecretKey"];
            _sendGridClient = sendGridClient ?? new SendGridClient(SendGridSecret);
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var from = new EmailAddress("emil.arrenius@student.kyh.se", "Kund");
            var to = new EmailAddress("ops@artslogistics.se");

            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);

            var response = await _sendGridClient.SendEmailAsync(msg);

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
