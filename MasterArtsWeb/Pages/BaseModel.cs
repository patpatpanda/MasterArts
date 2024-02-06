using MasterArtsLibrary.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace MasterArtsWeb.Pages
{
    public class BaseModel : PageModel
    {
        private readonly LanguageService _languageService;

        public BaseModel(LanguageService languageService)
        {
            _languageService = languageService;
        }

        public string CurrentLanguage { get; set; }

        public void OnGet()
        {
            // Hämta användarens språkpreferens från LanguageService
            CurrentLanguage = _languageService.GetCurrentLanguage();

            // Använd användarens språkpreferens för att anpassa sidans innehåll
            ViewData["Language"] = CurrentLanguage;
        }

        public IActionResult OnPost()
        {
            // Använd LanguageService för att byta språk och få det nya språket
            CurrentLanguage = _languageService.ToggleLanguage();

            // Spara det nya språket i ViewData för att användas i Razor-sidan
            ViewData["Language"] = CurrentLanguage;

            // Ladda om sidan för att tillämpa det nya språket
            Console.WriteLine($"Language switched to: {CurrentLanguage}");
            return RedirectToPage();
        }
    }
}
