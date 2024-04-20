using MasterArtsLibrary.Models;
using MasterArtsLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace MasterArtsWeb.Pages.UpsApi
{
    public class upsModel : PageModel
    {
        private readonly OrderService _upsRateService; // Make sure this service is correctly injected and configured
        [BindProperty]
        public RateRequest RateRequest { get; set; } // This should bind to the form data
        public RateResponse Response { get; set; }
        public string ApiResponse { get; set; }
        public string FormattedResponse { get; set; }
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CalculateRatesModel> _logger;
        public upsModel(OrderService upsRateService,  ILogger<CalculateRatesModel> logger )
        {
            _upsRateService = upsRateService;
            _logger = logger;
        }

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var rateRequest = new RateRequest
            {
                Request = new RequestDetails
                {
                    TransactionReference = new TransactionReference { CustomerContext = "CustomerContext" }
                },
                Shipment = new Shipment
                {
                    Shipper = new Party
                    {
                        Name = "ShipperName",
                        Address = new Address
                        {
                            AddressLine = new List<string> { "ShipperAddressLine1", "ShipperAddressLine2", "ShipperAddressLine3" },
                            City = "TIMONIUM",
                            StateProvinceCode = "MD",
                            PostalCode = "21093",
                            CountryCode = "US"
                        }
                    },
                    ShipTo = new Party
                    {
                        Name = "ShipToName",
                        Address = new Address
                        {
                            AddressLine = new List<string> { "ShipToAddressLine1", "ShipToAddressLine2", "ShipToAddressLine3" },
                            City = "Alpharetta",
                            StateProvinceCode = "GA",
                            PostalCode = "30005",
                            CountryCode = "US"
                        }
                    },
                    Service = new ServiceDetails { Code = "03", Description = "Ground" },
                    NumOfPieces = "1",
                    Package = new PackageDetails
                    {
                        PackagingType = new PackagingType { Code = "02", Description = "Packaging" },
                        Dimensions = new Dimensions
                        {
                            UnitOfMeasurement = new UnitOfMeasurement { Code = "IN", Description = "Inches" },
                            Length = "5",
                            Width = "5",
                            Height = "5"
                        },
                        PackageWeight = new PackageWeight
                        {
                            UnitOfMeasurement = new UnitOfMeasurement { Code = "LBS", Description = "Pounds" },
                            Weight = "1"
                        }
                    }
                }
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = null // Ensure PascalCase in JSON output
            };

            var jsonRequest = JsonSerializer.Serialize(new { RateRequest = rateRequest }, options);

            try
            {
                Response = await _upsRateService.GetShippingRatesAsync(jsonRequest);
                if (Response == null)
                {
                    _logger.LogError("Response from GetShippingRatesAsync is null.");
                    ViewData["ErrorResponse"] = "Failed to retrieve valid response.";
                }
                else
                {
                    ViewData["ApiResponse"] = JsonSerializer.Serialize(Response, new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = null });
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorResponse"] = $"An unexpected error occurred: {ex.Message}";
            }

            return Page();
        
    }

        private string FormatResponse(object response)
        {
            // Format the response in a way that is suitable for display or further processing
            return response.ToString();  // Placeholder: implement actual formatting logic based on response structure
        }
    }
}
