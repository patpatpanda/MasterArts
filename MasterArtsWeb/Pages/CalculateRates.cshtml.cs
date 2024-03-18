using MasterArtsLibrary.Models;
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
        private readonly ILogger<CalculateRatesModel> _logger; // Lägg till denna rad

        // Modifiera konstruktören för att ta emot ILogger via dependency injection
        public CalculateRatesModel(HttpClient client, ILogger<CalculateRatesModel> logger)
        {
            _client = client;
            _logger = logger; // Spara referensen till _logger

            // Din befintliga konfiguration...
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("nils-emil1337@hotmail.se:27wH5AFp")));
        }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var shippingRequest = new ShippingRequest
            {
                Module = "ocean",
                ImportExport = "export",
                Type = "lcl",
                FromCode = "SESTO",
                ToCode = "GBLON",
                RoutingCode = "",
                Imo = false,
                InlandZipCode = "41520",
                Packages = 10,
                PackageType = "PALLET(S)",
                Weight = 2000,
                Volume = 28.0,
                Date = "2024-04-01",
                Dimensions = new List<Dimension>
    {
        new Dimension
        {
            Pcs = 10,
            Length = 120,
            Width = 100,
            Height = 140,
            Weight = 200,
            Stackable = true
        }
    },
                Options = new List<Option>
    {
        new Option { Key = "vgm", Value = "true" },
        new Option { Key = "incoterms", Value = "dap" }
    }
            };

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };
            var jsonContent = JsonConvert.SerializeObject(shippingRequest, settings);

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.PostAsync("https://ncse.nordic-on.com/api/v1/calculaterateslcl", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    ApiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseString); // Deserialisera och tilldela här
                    _logger.LogInformation("Request lyckades: {ResponseString}", responseString);
                }

                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning("Request misslyckades med statuskod: {StatusCode}. Felmeddelande: {ErrorResponse}", response.StatusCode, errorResponse);
                    // Du kan överväga att även sätta detta som ett felmeddelande som visas på sidan.
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ett undantag inträffade när förfrågan gjordes");
            }


            return Page();
        }
    }
}
