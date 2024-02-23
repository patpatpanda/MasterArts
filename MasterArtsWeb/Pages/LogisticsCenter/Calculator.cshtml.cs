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

        [BindProperty]
        public List<ShipmentItem> Items { get; set; } = new List<ShipmentItem>();

        public string CurrentLanguage { get; set; }

        public double TotalVolume => Items.Sum(item => item.Volume * item.Pieces);

        public double TotalActualWeight => Items.Sum(item => item.ActualWeight * item.Pieces);

        public double TotalVolumeWeight => Items.Sum(item => item.VolumeWeight * item.Pieces);

        public double ChargeableWeight => Math.Max(TotalVolumeWeight, TotalActualWeight);

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

public class ShipmentItem
{
    public double Length { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double ActualWeight { get; set; }
    public int Pieces { get; set; }
    public string SelectedDensity { get; set; }
    public double Volume => Length * Width * Height / 1000000;

    public double VolumeWeight
    {
        get
        {
            double densityRatio = ExtractDensityRatio(SelectedDensity);
            return Volume * densityRatio * 1000;
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
