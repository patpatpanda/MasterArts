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

        [BindProperty]
        public ShippingRequest ShippingRequest { get; set; }

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

            _logger.LogInformation($"ShippingRequest är null: {ShippingRequest == null}");
            if (ShippingRequest != null)
            {
                _logger.LogInformation(JsonConvert.SerializeObject(ShippingRequest));
            }
            else
            {
                _logger.LogInformation("Ingen data bunden till ShippingRequest.");
            }
            // Serialisera shippingRequest till JSON med CamelCase för egenskapsnamn
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };
            var jsonContent = JsonConvert.SerializeObject(ShippingRequest, settings);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                // Skicka den serialiserade JSON-strängen till API:et
                var response = await _client.PostAsync("https://ncse.nordic-on.com/api/v1/calculaterateslcl", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    ApiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseString);
                   

                }
                else
                {
                    // Logga och hantera icke-lyckade svar
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, "Servern returnerade ett fel: " + errorResponse);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ett undantag inträffade: " + ex.Message);
            }

            return Page(); // Återvänd till sidan för att visa fel eller omformulera
        }

    }
}
