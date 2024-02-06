using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Services
{
    public interface IOrderEmailSender
    {
        Task SendEmailOrderAsync(string recipientEmail, string subject, string htmlMessage);
    }
}
