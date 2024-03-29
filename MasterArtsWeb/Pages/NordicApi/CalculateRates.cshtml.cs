using MasterArtsLibrary.Models;
using MasterArtsLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using System.Text;

namespace MasterArtsWeb.Pages
{
    public class CalculateRatesModel : PageModel
    {
        private readonly HttpClient _client;
        public ApiResponse ApiResponse { get; set; }

        [BindProperty]
        public ShippingRequest ShippingRequest { get; set; }
        public FclRate FclRequest { get; set; }
        private readonly OrderService _orderService;
        private readonly ILogger<CalculateRatesModel> _logger; // L�gg till denna rad

        // Modifiera konstrukt�ren f�r att ta emot ILogger via dependency injection
        public CalculateRatesModel(HttpClient client, ILogger<CalculateRatesModel> logger, OrderService order)
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
            _logger.LogInformation($"ShippingRequest �r null: {ShippingRequest == null}");
            if (ShippingRequest != null)
            {
                _logger.LogInformation(JsonConvert.SerializeObject(ShippingRequest));
                try
                {
                    var response = await _orderService.CalculateRatesAsync(ShippingRequest);
                    // Ytterligare logik efter anropet, som att hantera ApiResponse...
                    if (response is ApiResponse apiResponse)
                    {
                        // Hantera ApiResponse
                        ApiResponse = apiResponse; // Anta att ApiResponse �r av typen ApiResponse
                    }
                    else if (response is ApiResponseInland apiResponseInland)
                    {
                        // Hantera ApiResponseInland
                        // Om du vill konvertera ApiResponseInland till n�got som kan hanteras likadant som ApiResponse
                        // eller om du vill uppdatera UI baserat p� ApiResponseInland-specifika data
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Ett undantag intr�ffade: ", ex);
                    ModelState.AddModelError(string.Empty, $"Ett undantag intr�ffade: {ex.Message}");
                }
            }
            else
            {
                _logger.LogInformation("Ingen data bunden till ShippingRequest.");
                ModelState.AddModelError(string.Empty, "Ingen data bunden till ShippingRequest.");
            }

            return Page();
        }

    }
}