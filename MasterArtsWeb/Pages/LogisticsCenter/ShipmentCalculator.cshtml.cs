using MasterArtsLibrary.Models;
using MasterArtsLibrary.Services;
using MasterArtsLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace MasterArtsWeb.Pages.LogisticsCenter
{
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
        public double Length { get; set; }

        [BindProperty]
        public double Width { get; set; }

        [BindProperty]
        public double Height { get; set; }

        [BindProperty]
        public double ActualWeight { get; set; }

        [BindProperty]
        public int Pieces { get; set; }

        [BindProperty]
        public string SelectedDensity { get; set; }

        public double TotalVolume => Length * Width * Height / 1000000;

        public double VolumeWeightPerPiece => TotalVolume * 1000 / Pieces;

        // Uppdaterad beräkning av TotalVolumeWeight utan att konvertera till kubikmeter
        public double TotalVolumeWeight
        {
            get
            {
                double densityRatio = ExtractDensityRatio(SelectedDensity);
                if (densityRatio > 0)
                {
                    // Använd valt densitetsförhållande i beräkningen, multiplicera med 1000 och avrunda till en decimal
                    return Math.Round(TotalVolume * densityRatio * Pieces * 1000, 1);
                }
                else
                {
                    // Använd den befintliga logiken om inget valt densitetsförhållande, multiplicera med 1000 och avrunda till en decimal
                    return Math.Round(Length * Width * Height * Pieces * 1000 / 6, 1);
                }
            }
        }

        // Formatera TotalVolumeWeight med en decimalpunkt
        public string FormattedTotalVolumeWeight => TotalVolumeWeight.ToString("F1");



        public double TotalActualWeight => ActualWeight * Pieces;

        public double ChargeableWeight => TotalVolumeWeight > TotalActualWeight ? TotalVolumeWeight : TotalActualWeight;

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

                // Skapa order i API
                await _orderService.CreateOrderInApi(Order);

                var recipientEmail = Order.Consignor.ConsignorEmail;
                await _orderService.SendOrderConfirmationEmail(recipientEmail, Order);

                TempData["SuccessMessage"] = "Your order has been sent";
                return RedirectToPage("/Orders/CreateOrder");
            }

            return Page();
           
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
