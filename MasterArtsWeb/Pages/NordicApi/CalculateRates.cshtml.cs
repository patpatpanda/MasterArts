using DocumentFormat.OpenXml.Drawing.Charts;
using MasterArtsLibrary.Models;
using MasterArtsLibrary.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using System.Text;
using Order = MasterArtsLibrary.Models.Order;

namespace MasterArtsWeb.Pages
{
    public class CalculateRatesModel : PageModel
    {
        private readonly HttpClient _client;
        public ApiResponse ApiResponse { get; set; }
        private readonly MyDbContext _context;

        [BindProperty]
        public ShippingRequest ShippingRequest { get; set; }
        public CustomerRates CustomerRates { get; set; }
        public string CustomerNumber { get; set; }
        private readonly UserManager<IdentityUser> _userManager;
        private readonly OrderService _orderService;
        private readonly ILogger<CalculateRatesModel> _logger; // Lägg till denna rad

        // Modifiera konstruktören för att ta emot ILogger via dependency injection
        public CalculateRatesModel(HttpClient client, ILogger<CalculateRatesModel> logger, OrderService order,MyDbContext context, UserManager<IdentityUser> userManager)
        {
            _client = client;
            _logger = logger; // Spara referensen till _logger
            _orderService = order;
            _context = context;
            _userManager = userManager;
            // Din befintliga konfiguration...

        }


        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userManager.GetUserId(User);
            CustomerNumber = await GetCustomerNumberAsync(userId);  // Hämta CustomerNumber utan att direkt avbryta processen

            _logger.LogInformation($"ShippingRequest är null: {ShippingRequest == null}");
            if (ShippingRequest == null)
            {
                _logger.LogInformation("Ingen data bunden till ShippingRequest.");
                ModelState.AddModelError(string.Empty, "Ingen data bunden till ShippingRequest.");
                return Page();
            }

            _logger.LogInformation(JsonConvert.SerializeObject(ShippingRequest));
            try
            {
                var response = await _orderService.CalculateRatesAsync(ShippingRequest);

                if (response is ApiResponse apiResponse)
                {
                    ApiResponse = apiResponse;
                    TempData["ApiResponse"] = JsonConvert.SerializeObject(apiResponse);
                    TempData.Keep("ApiResponse");

                    // Skapa och spara CustomerRates endast om det finns ett CustomerNumber
                    if (!string.IsNullOrWhiteSpace(CustomerNumber))
                    {
                        CustomerRates = new CustomerRates
                        {
                            ShippingRequest = ShippingRequest,
                            Totals = ApiResponse.Totals,
                            Rates = ApiResponse.Rates,
                            TransitTime = ApiResponse.TransitTime,
                            Sailing = ApiResponse.Sailing,
                            Agent = ApiResponse.Agent,
                            Co2 = ApiResponse.Co2,
                            ValidFrom = DateTime.Now.ToString("yyyy-MM-dd"),
                            ValidTo = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd"),
                            OnRequest = false,
                            CustomerOrderNumber = CustomerNumber  // Använda CustomerNumber som CustomerOrderNumber
                        };

                        _context.CustomerRates.Add(CustomerRates);
                        await _context.SaveChangesAsync();
                        _logger.LogInformation("Customer Rates sparades framgångsrikt.");
                    }
                    else
                    {
                        _logger.LogInformation("Inget giltigt kundnummer finns, ingen CustomerRates skapas.");
                    }
                    return Page();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Ett undantag inträffade: ", ex);
                ModelState.AddModelError(string.Empty, $"Ett undantag inträffade: {ex.Message}");
                return Page();
            }
            return Page();
        }



        private async Task<string> GetCustomerNumberAsync(string userId)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.UserId == userId);

            return customer?.CustomerNumber;
        }




        //public async Task<IActionResult> OnPostCreateOrderAsync(ShippingRequest request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        _logger.LogInformation($"Model state is invalid. Errors: {string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}");
        //        return Page(); // Returnera samma sida om formulärvalideringen misslyckas
        //    }

        //    Order newOrder = CreateOrderFromShippingRequest(ShippingRequest);

        //    _context.Orders.Add(newOrder);
        //    _context.SaveChanges();
        //    TempData["OrderId"] = newOrder.Id;
        //    foreach (var good in newOrder.Goods)
        //    {
        //        TempData["Description"] = good.Description; // Exempel: spara första godsets beskrivning
        //        break;
        //    }

        //    return RedirectToPage("/LogisticsCenter/ShipmentCalculator", new { orderId = newOrder.Id });
        //}
        //// Exempel på en metod som skapar en Order från en ShippingRequest
        //public Order CreateOrderFromShippingRequest(ShippingRequest request)
        //{
        //    var order = new Order
        //    {
        //        TransportModeCode = 11,
        //        ConsignorCity = request.FromCity,
        //        ConsigneeCity = request.ToCity,
        //        Goods = new List<Goods>()
        //    };


        //    if (request.Dimensions != null && request.Dimensions.Count > 0)
        //    {
        //        foreach (var dimension in request.Dimensions)
        //        {
        //            var good = new Goods
        //            {
        //                Description = "Auto-generated from dimensions",
        //                Quantity = dimension.Pcs,
        //                Length = dimension.Length,
        //                Height = dimension.Height,
        //                Width = dimension.Width,
        //                NetWeight = dimension.Weight,

        //                GrossWeight = dimension.Weight, // Om grossWeight ska beräknas annorlunda, justera detta
        //                Remarks = dimension.Stackable ? "Stackable" : "Non-stackable",
        //                PackageType = "Default" // Sätt en standardvärde eller dynamisk logik baserat på dimension eller andra faktorer
        //            };
        //            order.Goods.Add(good);
        //        }
        //    }

        //    return order;
        //}





    }
}