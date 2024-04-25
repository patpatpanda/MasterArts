using DocumentFormat.OpenXml.Bibliography;
using MasterArtsLibrary.Models;
using MasterArtsLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace MasterArtsWeb.Pages.UpsApi
{
    [Authorize]
    public class upsModel : PageModel
    {
        private readonly OrderService _upsRateService; // Make sure this service is correctly injected and configured
        [BindProperty]
        public RateRequest RateRequest { get; set; } = new RateRequest();
        public RateResponse Response { get; set; }
        
        public string FormattedResponse { get; set; }
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CalculateRatesModel> _logger;
        public upsModel(OrderService upsRateService, ILogger<CalculateRatesModel> logger)
        {
            _upsRateService = upsRateService;
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Loggar alla fel i ModelState
                foreach (var entry in ModelState)
                {
                    if (entry.Value.Errors.Count > 0)
                    {
                        _logger.LogError($"Errors for {entry.Key}:");
                        foreach (var error in entry.Value.Errors)
                        {
                            _logger.LogError(error.ErrorMessage);
                        }
                    }
                }
            }
            try
            {
                // Ange TaxInformationIndicator
            

                var options = new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = null };
                var jsonRequest = JsonSerializer.Serialize(new { RateRequest }, options);

                var response = await _upsRateService.GetShippingRatesAsync(jsonRequest);
                if (response != null)
                {
                    Response = response;
                    ViewData["ApiResponse"] = JsonSerializer.Serialize(response, options);
                }
                else
                {
                    _logger.LogError("No response received from the UPS service.");
                    ViewData["ErrorResponse"] = "Failed to retrieve a valid response from the service.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}");
                ViewData["ErrorResponse"] = $"An unexpected error occurred: {ex.Message}";
            }

            return Page();
        }
    }
    }
