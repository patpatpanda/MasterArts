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

                var subject = "Contact Form Submission";
                var htmlMessage = $"Företag: {CompanyName}<br/> ----------- <br/> ----------- <br/>Namn: {Name}<br/> ----------- <br/>Telefon: {Phone}<br/> ----------- <br/>Email: {Email}<br/> ----------- <br/>Meddelande: {Message}";

                // Send the email using the built-in IEmailSender
                await _emailSender.SendEmailAsync(Email, subject, htmlMessage);

                // Log success message for debugging purposes
                Debug.WriteLine("Form submission successful");

                return RedirectToPage("/Contact");
            }
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
