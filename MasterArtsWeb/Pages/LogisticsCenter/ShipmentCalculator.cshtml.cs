using MasterArtsLibrary.Models;
using MasterArtsLibrary.Services;
using MasterArtsLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace MasterArtsWeb.Pages.LogisticsCenter
{
    [Authorize]
    public class ShipmentCalculatorModel : PageModel
    {

        public ShipmentCalculatorModel(LanguageService languageService, IHttpClientFactory clientFactory,OrderService orderService)
                
        {
            _clientFactory = clientFactory;
            _languageService = languageService;
            _orderService = orderService;
        }
        private readonly OrderService _orderService;
        [BindProperty]
        public Order Order { get; set; }
       

        private readonly IHttpClientFactory _clientFactory;
        [BindProperty]
       
        public CurrencyExchangeRates CurrencyData { get; set; }
        public string BaseCurrency { get; set; } = "SEK";
        private readonly LanguageService _languageService;
        public string CurrentLanguage { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var countries = await _orderService.GetAllCountries();
            ViewData["Countries"] = countries;

            CurrentLanguage = _languageService.GetCurrentLanguage();
            var client = _clientFactory.CreateClient();
            var response = await client.GetStringAsync($"https://api.exchangerate-api.com/v4/latest/{BaseCurrency}");

            CurrencyData = JsonConvert.DeserializeObject<CurrencyExchangeRates>(response);
            return  Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var countries = await _orderService.GetAllCountries();
            ViewData["Countries"] = countries;
            CurrentLanguage = _languageService.ToggleLanguage();
            ViewData["Language"] = CurrentLanguage;

            if (ModelState.IsValid)
            {
                
                // Om modellen är giltig, fortsätt med att skapa order i API
                await _orderService.CreateOrderInApi(Order);
                var recipientEmail = Order.Consignor.ConsignorEmail;
                await _orderService.SendOrderConfirmationEmail(recipientEmail, Order);

                TempData["SuccessMessage"] = "Your order has been sent";
                return RedirectToPage("/LogisticsCenter/ShipmentCalculator");
            }
            else
            {
                // Om modellen är ogiltig, logga valideringsfel och återvänd till sidan
                foreach (var entry in ModelState.Values)
                {
                    foreach (var error in entry.Errors)
                    {
                        // Logga eller hantera valideringsfel här
                        Console.WriteLine($"Validation error: {error.ErrorMessage}");
                    }
                }

                return Page();
            }
        }


        private double ExtractDensityRatio(string selectedDensity)
        {
            double ratio = 0.0;
            if (!string.IsNullOrEmpty(selectedDensity))
            {
                string[] parts = selectedDensity.Split(':');
                if (parts.Length == 2 && double.TryParse(parts[0], out double numerator) && double.TryParse(parts[1], out double denominator))
                {
                    ratio = numerator / denominator;
                }
            }
            return ratio;
        }
    }
}
