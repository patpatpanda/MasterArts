using MasterArtsLibrary.Services;
using MasterArtsLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace MasterArtsWeb.Pages.LogisticsCenter
{
    public class ShipmentCalculatorModel : PageModel
    {

        public ShipmentCalculatorModel(LanguageService languageService, IHttpClientFactory clientFactory)
                
        {
            _clientFactory = clientFactory;
            _languageService = languageService;
        }

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

        // Uppdaterad ber�kning av TotalVolumeWeight utan att konvertera till kubikmeter
        public double TotalVolumeWeight
        {
            get
            {
                double densityRatio = ExtractDensityRatio(SelectedDensity);
                if (densityRatio > 0)
                {
                    // Anv�nd valt densitetsf�rh�llande i ber�kningen, multiplicera med 1000 och avrunda till en decimal
                    return Math.Round(TotalVolume * densityRatio * Pieces * 1000, 1);
                }
                else
                {
                    // Anv�nd den befintliga logiken om inget valt densitetsf�rh�llande, multiplicera med 1000 och avrunda till en decimal
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
            CurrentLanguage = _languageService.GetCurrentLanguage();
            var client = _clientFactory.CreateClient();
            var response = await client.GetStringAsync($"https://api.exchangerate-api.com/v4/latest/{BaseCurrency}");

            CurrencyData = JsonConvert.DeserializeObject<CurrencyExchangeRates>(response);
            return  Page();
        }
        public void OnPost()
        {
            // Do nothing on post for now
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
