using MasterArtsLibrary.Models;
using MasterArtsLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace MasterArtsWeb.Pages.NordicApi
{
    public class FclRequestModel : PageModel
    {
        private readonly HttpClient _client;
        public ApiResponse ApiResponse { get; set; }

        [BindProperty]
      
        public FclRate FclRequest { get; set; }
        private readonly OrderService _orderService;
        private readonly ILogger<FclRate> _logger;

        
        public FclRequestModel(HttpClient client, ILogger<FclRate> logger, OrderService order)
        {
            _client = client;
            _logger = logger; 
            _orderService = order;
            

        }


        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation($"ShippingRequest är null: {FclRequest == null}");
            if (FclRequest != null)
            {
                _logger.LogInformation(JsonConvert.SerializeObject(FclRequest));
                try
                {

                    ApiResponse = await _orderService.CalculateRatesFclAsync(FclRequest, RateType.FCL);

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

