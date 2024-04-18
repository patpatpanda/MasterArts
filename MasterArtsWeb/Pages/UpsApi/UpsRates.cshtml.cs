using MasterArtsLibrary.Models;
using MasterArtsLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MasterArtsWeb.Pages.UpsApi
{
    public class UpsRatesModel : PageModel
    {
        private readonly OrderService _upsRateService; // Make sure this service is correctly injected and configured
        [BindProperty]
        public RateRequest RateRequest { get; set; } // This should bind to the form data

        public string ApiResponse { get; set; }
        public string FormattedResponse { get; set; }
        private readonly IHttpClientFactory _httpClientFactory;
        public UpsRatesModel(OrderService upsRateService)
        {
            _upsRateService = upsRateService;
        }

        public void OnGet()
        {
            // Method to initialize any data or prepare the page for display
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelStateKey in ViewData.ModelState.Keys)
                {
                    var modelStateVal = ViewData.ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        // Logga error.ErrorMessage eller hantera felet
                        Console.WriteLine($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
                    }
                }

            }


            try
            {
                // Assuming GetShippingRatesAsync accepts a RateRequest and returns a RateResponse
                var response = await _upsRateService.GetShippingRatesAsync(RateRequest);
                ApiResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });

                // Format the response to be user-friendly or for further processing
                FormattedResponse = FormatResponse(response);
            }
            catch (Exception ex)
            {
                // Log and handle exceptions
                ApiResponse = $"An error occurred: {ex.Message}";
                ModelState.AddModelError(string.Empty, ApiResponse);
            }

            return Page(); // Return the page to display results or errors
        }

        private string FormatResponse(RateResponse response)
        {
            // Format the response for display, assuming response has the necessary structure
            if (response?.RatedShipment != null && response.RatedShipment.Any())
            {
                var firstRate = response.RatedShipment.First();
                return $"Service: {firstRate.Service.Description}, Charge: {firstRate.TotalCharges.MonetaryValue}";
            }
            return "No rates available or invalid response.";
        }
    }
}
