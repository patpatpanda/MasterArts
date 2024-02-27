using MasterArtsLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace MasterArtsWeb.Pages.LogisticsCenter
{
    public class CalculatorModel : PageModel
    {
        private readonly LanguageService _languageService;
        private readonly IHttpClientFactory _clientFactory;

        public CalculatorModel(LanguageService languageService, IHttpClientFactory clientFactory)
        {
            _languageService = languageService;
            _clientFactory = clientFactory;
        }

        
        

        public string CurrentLanguage { get; set; }

       

        public void OnGet()
        {
            CurrentLanguage = _languageService.GetCurrentLanguage();
        }

        public void OnPost()
        {
            CurrentLanguage = _languageService.ToggleLanguage();
            ViewData["Language"] = CurrentLanguage;

           
        }
    }
}



