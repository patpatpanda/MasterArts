using MasterArtsLibrary.Services;
using MasterArtsLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace MasterArtsWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly LanguageService _languageService;
       
        private readonly IHttpClientFactory _clientFactory;
        public IndexModel(LanguageService languageService,  IHttpClientFactory clientFactory)
        {
            _languageService = languageService;
           
            _clientFactory = clientFactory;
        }
        public bool IsDayTime { get; private set; }
        public string WeatherDescription { get; private set; }


       
        public string CurrentLanguage { get; set; }
        public ExchangeRateModel ExchangeRate { get; private set; }
        public CurrencyExchangeRates CurrencyData { get; set; }
        public string BaseCurrency { get; set; } = "SEK";
        public async Task<IActionResult> OnGetAsync()

        {

            CurrentLanguage = _languageService.GetCurrentLanguage();

            // Använd användarens språkpreferens för att anpassa sidans innehåll
            ViewData["Language"] = CurrentLanguage;

            
           

            DateTime currentTime = DateTime.Now;
            int hour = currentTime.Hour;
            IsDayTime = hour >= 6 && hour < 18;
            

            // Hämta valutainformation för exempelvis SEK till USD
            // ...

            // Hämta valutainformation för exempelvis SEK till EUR
            // ExchangeRate = await _weather.GetExchangeRateAsync("SEK", "SGD");

            // ...
            var client = _clientFactory.CreateClient();
            var response = await client.GetStringAsync($"https://api.exchangerate-api.com/v4/latest/{BaseCurrency}");

            CurrencyData = JsonConvert.DeserializeObject<CurrencyExchangeRates>(response);





            return Page();
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