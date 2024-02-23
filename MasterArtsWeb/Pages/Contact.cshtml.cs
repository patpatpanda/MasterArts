using MasterArtsLibrary.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace MasterArtsWeb.Pages
{
    public class ContactModel : PageModel
    {

        public string CurrentLanguage { get; set; }
        private readonly IEmailSender _emailSender;
        private readonly LanguageService _languageService;
        public string CompanyName { get; set; }
        [BindProperty]

        public string Name { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Phone is required.")]
        public string Phone { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Message is required.")]
        [MinLength(5, ErrorMessage = "Message must be at least 5 characters long.")]
        public string Message { get; set; }
        public ContactModel(IEmailSender emailSender, LanguageService languageService)
        {
            _emailSender = emailSender;
            _languageService = languageService;
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
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
                    .Replace("{{CompanyName}}", CompanyName)
                    .Replace("{{Name}}", Name)
                    .Replace("{{Phone}}", Phone)
                    .Replace("{{Email}}", Email)
                    .Replace("{{Message}}", Message);

                // Skicka e-postmeddelandet
                await _emailSender.SendEmailAsync(Email, subject, htmlMessage);

                // Logga framgångsmeddelandet för felsökningsändamål
                Debug.WriteLine("Form submission successful");

                return RedirectToPage("/Contact");
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

    }
}
