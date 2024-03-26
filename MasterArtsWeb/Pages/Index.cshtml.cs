
using MasterArtsLibrary.Models;
using MasterArtsLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;





namespace MasterArtsWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly LanguageService _languageService;
        private readonly IHttpClientFactory _clientFactory;
        private readonly MyDbContext _context;
        private readonly OrderService _orderService;


        public IndexModel(LanguageService languageService, IHttpClientFactory clientFactory, MyDbContext dbContext,OrderService orderService)
        {
            _languageService = languageService;
            _clientFactory = clientFactory;
            _context = dbContext;
            _orderService = orderService;


        }


        public string CurrentLanguage { get; set; }

        public string TrackCode { get; set; }
        public TransportInformation TrackResponse { get; set; }

        public void  OnGet()
        {

            CurrentLanguage = _languageService.GetCurrentLanguage();
            ViewData["Language"] = CurrentLanguage;



           
        }

        public async Task<IActionResult> OnPostAsync(string trackingCode)
        {
            TrackResponse = null;
            TempData.Remove("TrackResponse");

            if (string.IsNullOrWhiteSpace(trackingCode))
            {
                // Om inget trackingCode anges
                TempData["NotValidCode"] = "Tracking code is required.";
                return RedirectToPage(new { scrollTo = "contact" });
            }

            try
            {
                TrackResponse = await _orderService.TrackAndTraceAsync(trackingCode);
                if (TrackResponse == null)
                {
                    // Om trackingCode inte ger något resultat
                    TempData["NotValidCode"] = "Could not find anything.";
                    return RedirectToPage(new { scrollTo = "contact" }); // Använd RedirectToPage för att uppdatera sidan med TempData
                }

                // Här kan du hantera ett framgångsrikt sökresultat
                // Exempelvis genom att spara resultatet i TempData eller på något annat sätt
            }
            catch (Exception ex)
            {
                // Hantera eventuella undantag som kastas under processen
                TempData["NotValidCode"] = $"Could not find anything";
                return Page();
            }

            CurrentLanguage = _languageService.ToggleLanguage();
            ViewData["Language"] = CurrentLanguage;
            Console.WriteLine($"Language switched to: {CurrentLanguage}");

            
            return Page();
        }

        public IActionResult OnPostClear()
        {
            // Rensa TrackResponse-informationen
            TrackResponse = null;
            TempData.Remove("TrackResponse"); // Om du använder TempData för att lagra TrackResponse

            // Omdirigera till samma sida för att uppdatera vyn och ta bort visningen av TrackResponse
            return RedirectToPage();
        }


    }
}