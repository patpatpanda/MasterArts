using MasterArtsLibrary.Models;
using MasterArtsLibrary.Services;
using MasterArtsLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MasterArtsWeb.Pages.LogisticsCenter
{
    [Authorize]
    public class ShipmentCalculatorModel : PageModel
    {

        public ShipmentCalculatorModel(LanguageService languageService, IHttpClientFactory clientFactory, OrderService orderService, UserManager<IdentityUser> userManager, MyDbContext context, ILogger<ShipmentCalculatorModel> logger)

        {
            _clientFactory = clientFactory;
            _languageService = languageService;
            _orderService = orderService;
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }
        private readonly UserManager<IdentityUser> _userManager;
        private readonly OrderService _orderService;
        private readonly ILogger<ShipmentCalculatorModel> _logger;
        [BindProperty]
        public Order Order { get; set; } = new Order();
        private readonly MyDbContext _context;
        public int OrderId { get; set; }
        public string Description { get; set; }
        public PartialViewResult OnGetAddGoodsForm(int index)
        {
            var newGoods = new Goods(); // Initialize your Goods model
                                        // You may set default values or adjust the model as needed here
            return Partial("_GoodsFormPartial", newGoods);
        }

        private readonly IHttpClientFactory _clientFactory;
        [BindProperty]

        public CurrencyExchangeRates CurrencyData { get; set; }
        public string BaseCurrency { get; set; } = "SEK";
        private readonly LanguageService _languageService;
        public string CurrentLanguage { get; set; }
        public string CustomerNumber { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue)
            {
                // Använd Include() för att inkludera Goods listan i den hämtade ordern
                Order = await _context.Orders
                                       .Include(o => o.Goods) // Inkludera Goods-listan
                                       .FirstOrDefaultAsync(o => o.Id == id);

                if (Order == null)
                {
                    return NotFound("Order with the specified ID not found.");
                }
            }
            else
            {
                // Inget ID tillhandahållet, skapa en ny tom order
                Order = new Order();
            }
            return Page();
        }



        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation($"Order received: {JsonConvert.SerializeObject(Order)}");

           
            
            CurrentLanguage = _languageService.ToggleLanguage();
            ViewData["Language"] = CurrentLanguage;

            if (ModelState.IsValid)
            {
                _logger.LogInformation($"Order received: {JsonConvert.SerializeObject(Order)}");

                await _orderService.CreateOrderInApi(Order);
                //var recipientEmail = Order.Consignor.ConsignorEmail;
                //await _orderService.SendOrderConfirmationEmail(recipientEmail, Order);

                TempData["SuccessMessage"] = "Your order has been sent";
                return RedirectToPage("/LogisticsCenter/ShipmentCalculator");
            }
            else
            {
                // Om modellen är ogiltig, logga valideringsfel och återvänd till sidan
                foreach (var entry in ModelState.Values)
                {
                    foreach (var error in entry.Errors)
                    {
                        // Logga eller hantera valideringsfel här
                        Console.WriteLine($"Validation error: {error.ErrorMessage}");
                    }
                }

                return Page();
            }
        }

        public async Task<string> GetCustomerNumberAsync(string userId)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.UserId == userId);

            return customer?.CustomerNumber;
        }

       
    }
}