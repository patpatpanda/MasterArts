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

        public ShipmentCalculatorModel(LanguageService languageService, IHttpClientFactory clientFactory,OrderService orderService, UserManager<IdentityUser> userManager,MyDbContext context)
                
        {
            _clientFactory = clientFactory;
            _languageService = languageService;
            _orderService = orderService;
            _userManager = userManager;
            _context = context;
        }
        private readonly UserManager<IdentityUser> _userManager;
        private readonly OrderService _orderService;
        [BindProperty]
        public Order Order { get; set; } = new Order();
        private readonly MyDbContext _context;


        private readonly IHttpClientFactory _clientFactory;
        [BindProperty]
       
        public CurrencyExchangeRates CurrencyData { get; set; }
        public string BaseCurrency { get; set; } = "SEK";
        private readonly LanguageService _languageService;
        public string CurrentLanguage { get; set; }
        public string CustomerNumber { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (Order == null)
            {
                Order = new Order();
            }

            if (Order.Goods == null || !Order.Goods.Any())
            {
                // Lägg till en tom Goods-instans så att det finns något att binda till i vyn
                Order.Goods.Add(new Goods());
            }
            var userId = _userManager.GetUserId(User);
            CustomerNumber = await GetCustomerNumberAsync(userId);

            var countries = await _orderService.GetAllCountries();
            ViewData["Countries"] = countries;

            CurrentLanguage = _languageService.GetCurrentLanguage();
            var client = _clientFactory.CreateClient();
            var response = await client.GetStringAsync($"https://api.exchangerate-api.com/v4/latest/{BaseCurrency}");
            
            CurrencyData = JsonConvert.DeserializeObject<CurrencyExchangeRates>(response);
            return  Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (Order.Goods == null)
            {
                Order.Goods = new List<Goods>(); // Initialisera om den är null
            }
            var countries = await _orderService.GetAllCountries();
            ViewData["Countries"] = countries;
            CurrentLanguage = _languageService.ToggleLanguage();
            ViewData["Language"] = CurrentLanguage;

            if (ModelState.IsValid)
            {
                
                
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
