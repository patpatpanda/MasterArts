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
        private readonly ILogger<CalculateRatesModel> _logger; // Lägg till denna rad

        // Modifiera konstruktören för att ta emot ILogger via dependency injection
        public CalculateRatesModel(HttpClient client, ILogger<CalculateRatesModel> logger,OrderService order)
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
            _logger.LogInformation($"ShippingRequest är null: {ShippingRequest == null}");
            if (ShippingRequest != null)
            {
                _logger.LogInformation(JsonConvert.SerializeObject(ShippingRequest));
                try
                {
                    
                    ApiResponse = await _orderService.CalculateRatesAsync(ShippingRequest, RateType.LCL);

                    if (ApiResponse == null)
                    {
                        _logger.LogInformation("API-svaret är null.");
                        ModelState.AddModelError(string.Empty, "Servern returnerade ett fel eller inget svar.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Ett undantag inträffade: ", ex);
                    ModelState.AddModelError(string.Empty, $"Ett undantag inträffade: {ex.Message}");
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
