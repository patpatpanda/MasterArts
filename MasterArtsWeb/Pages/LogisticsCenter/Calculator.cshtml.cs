using MasterArtsLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MasterArtsWeb.Pages.LogisticsCenter
{
    public class CalculatorModel : PageModel
    {
        public CalculatorModel(LanguageService languageService)

        {
           
            _languageService = languageService;
           
        }
        private readonly LanguageService _languageService;
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
        public string CurrentLanguage { get; set; }
        public void OnGet()
        {
            CurrentLanguage = _languageService.GetCurrentLanguage();
        }
        public void  OnPost()
        {
            CurrentLanguage = _languageService.ToggleLanguage();
            ViewData["Language"] = CurrentLanguage;
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
