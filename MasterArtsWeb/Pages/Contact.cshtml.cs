using MasterArtsLibrary.Models;
using MasterArtsLibrary.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace MasterArtsWeb.Pages
{
    public class ContactModel : PageModel
    {

       
        private readonly IEmailSender _emailSender;
        
        [BindProperty]
        public ContactFormModel Input { get; set; }
        public ControllerContext ControllerContext { get; set; }

        public ContactModel(IEmailSender emailSender)
        {
            _emailSender = emailSender;
            
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {

            if (!string.IsNullOrEmpty(Input.Honeypot) ||
    !IsValidEmail(Input.Email) ||
    ContainsStopWords(Input.Message) || // Fixade parentes
    !IsValidPhoneNumber(Input.Phone)) // Fixade syntax för metodanrop och borttagning av '= false'
            
                // Här kan du hantera fallet när valideringen misslyckas
                // Till exempel, skicka tillbaka formuläret med ett felmeddelande
            

            {
                // Antag att det är ett botförsök och hantera det enligt önskemål
                // Exempelvis genom att logga försöket och returnera samma sida
                return Page();
            }
            ModelState.Remove("Input.Honeypot");
            if (ModelState.IsValid)
            {
                TempData["SuccessMessage"] = "Your message has been sent";

                // Definiera ämnet för e-postmeddelandet
                var subject = "Contact Form Submission";

                // Definiera HTML-mallen
                string htmlTemplate = @"<!DOCTYPE html>
        <html>
        <head>
            <style>
                .container {
                    font-family: Arial, sans-serif;
                    background-color: #f4f4f4;
                    padding: 20px;
                    border-radius: 10px;
                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                }
                .header {
                    background-color: #007bff;
                    color: white;
                    padding: 10px;
                    border-radius: 10px 10px 0 0;
                    text-align: center;
                }
                .content p {
                    color: #333;
                }
            </style>
        </head>
        <body>
            <div class='container'>
                <div class='header'>
                    <h2>Kontaktformulär Submission</h2>
                </div>
                <div class='content'>
                    <p><strong>Företagsnamn:</strong> {{CompanyName}}</p>
                    <p><strong>Namn:</strong> {{Name}}</p>
                    <p><strong>Telefon:</strong> {{Phone}}</p>
                    <p><strong>Email:</strong> {{Email}}</p>
                    <p><strong>Meddelande:</strong> {{Message}}</p>
                </div>
            </div>
        </body>
        </html>";

                // Ersätt placeholders med faktiska data
                var htmlMessage = htmlTemplate
                    .Replace("{{CompanyName}}", Input.CompanyName)
                    .Replace("{{Name}}", Input.Name)
                    .Replace("{{Phone}}", Input.Phone)
                    .Replace("{{Email}}", Input.Email)
                    .Replace("{{Message}}", Input.Message);

                // Skicka e-postmeddelandet
                await _emailSender.SendEmailAsync(Input.Email, subject, htmlMessage);

                // Logga framgångsmeddelandet för felsökningsändamål
                Debug.WriteLine("Form submission successful");

                return RedirectToPage(new { scrollTo = "contact" });

            }
            // Hantera fall där ModelState inte är giltig



            else
            {
                // Log validation errors for debugging purposes
                Debug.WriteLine("Validation errors:");
                foreach (var modelStateEntry in ModelState.Values)
                {
                    foreach (var error in modelStateEntry.Errors)
                    {
                        Debug.WriteLine($"Model error: {error.ErrorMessage}");
                    }
                }

                TempData["ErrorMessage"] = "Please correct the validation errors.";
                return Page();
            }
        }
        
        
            public static bool IsValidPhoneNumber(string phoneNumber)
            {
                if (string.IsNullOrWhiteSpace(phoneNumber))
                    return false;

                // Detta är ett enkelt exempel på regex som kan validera många internationella telefonnummerformat
                // Det tillåter valfritt antal inledande plustecken följt av siffror, med tillåtelse för mellanslag, punkter och bindestreck.
                var regex = new Regex(@"^\+?[0-9\s\-\.\(\)]+$");

                return regex.IsMatch(phoneNumber);
            }
        
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Kontrollera om meddelandet innehåller stoppord
        bool ContainsStopWords(string message)
        {
            var stopWords = new List<string> { "http", "www", "link", "url", "spamWord1", "spamWord2" }; // Uppdatera med relevanta stoppord
            foreach (var word in stopWords)
            {
                if (message.Contains(word, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
