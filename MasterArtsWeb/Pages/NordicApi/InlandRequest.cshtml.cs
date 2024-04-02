using MasterArtsLibrary.Models;
using MasterArtsLibrary.Services;
using MasterArtsLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace MasterArtsWeb.Pages.NordicApi
{
    public class InlandRequestModel : PageModel
    {
        
        
            private readonly HttpClient _client;
            public ApiResponse ApiResponse { get; set; }
        public ApiResponseInland ApiResponseInland { get; set; }

        [BindProperty]
            public ShippingRequest ShippingRequest { get; set; }
        [BindProperty]
        public InlandViewModel Inland { get; set; } = new InlandViewModel();

        
        public FclRate FclRequest { get; set; }
            private readonly OrderService _orderService;
            private readonly ILogger<CalculateRatesModel> _logger; // Lägg till denna rad

            // Modifiera konstruktören för att ta emot ILogger via dependency injection
            public InlandRequestModel(HttpClient client, ILogger<CalculateRatesModel> logger, OrderService order)
            {
                _client = client;
                _logger = logger; // Spara referensen till _logger
                _orderService = order;
                // Din befintliga konfiguration...

            }


            public void OnGet()
            {

            }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Inland != null)
            {
                var shippingRequest = new ShippingRequest
                {
                    Module = Inland.Module,
                    ImportExport = Inland.ImportExport,
                    Type = Inland.Type,
                    InlandZipCode = Inland.InlandZipCode,
                    Packages = Inland.Packages,
                  
                    Weight = Inland.Weight,
                    Volume = Inland.Volume,
                    Date = Inland.Date,
                    City = Inland.City,
                    Dimensions = Inland.Dimensions,
                   UserSelection = Inland.UserSelection
                };

                try
                {
                    var response = await _orderService.CalculateRatesAsync(shippingRequest);
                    // Ytterligare logik efter anropet, som att hantera ApiResponse...
                    if (response is ApiResponse apiResponse)
                    {
                        // Hantera ApiResponse
                        ApiResponse = apiResponse; // Anta att ApiResponse är av typen ApiResponse
                    }
                    else if (response is ApiResponseInland apiResponseInland)
                    {
                        ApiResponseInland = apiResponseInland;
                    }
                }
                catch (Exception ex)
                {
                    // Hantera undantag
                    _logger.LogError("Ett undantag inträffade: ", ex);
                }
            }
            else
            {
                _logger.LogInformation("InlandViewModel är null.");
            }

            return Page(); // Eller någon annan lämplig IActionResult
        }


    }
}

