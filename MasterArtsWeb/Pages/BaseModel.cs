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
           
            CurrentLanguage = _languageService.GetCurrentLanguage();

           
            ViewData["Language"] = CurrentLanguage;
        }

        public IActionResult OnPost()
        {
           
            CurrentLanguage = _languageService.ToggleLanguage();

            
            ViewData["Language"] = CurrentLanguage;

           
            Console.WriteLine($"Language switched to: {CurrentLanguage}");
            return RedirectToPage();
        }
    }
}
