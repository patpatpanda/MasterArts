using MasterArtsLibrary.Models;
using MasterArtsLibrary.Services;
using MasterArtsLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace MasterArtsWeb.Pages.LogisticsCenter
{
    [Authorize]
    public class ShipmentCalculatorModel : PageModel
    {

        public ShipmentCalculatorModel(IHttpClientFactory clientFactory, UserManager<IdentityUser> userManager, MyDbContext context, ILogger<ShipmentCalculatorModel> logger, IConfiguration configuration, OrderService orderService)
        {
            _clientFactory = clientFactory;
            _userManager = userManager;
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _orderService = orderService; // Initialisera _orderService
        }

        private readonly UserManager<IdentityUser> _userManager;
        private readonly OrderService _orderService;
        private readonly ILogger<ShipmentCalculatorModel> _logger;
        [BindProperty]
        public Order Order { get; set; } = new Order();
        private readonly MyDbContext _context;
        public int OrderId { get; set; }
        private readonly IConfiguration _configuration;
        public string Description { get; set; }
        public IList<Order> Orders { get; set; }
        public bool IsOpsUser { get; set; }
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
       
        public string CurrentLanguage { get; set; }
        public string CustomerNumber { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue)
            {
                // H�mtar en order med dess varor om ett ID tillhandah�lls
                Order = await _context.Orders
                                       .Include(o => o.Goods)
                                       .FirstOrDefaultAsync(o => o.Id == id);

                if (Order == null)
                {
                    return NotFound("Order with the specified ID not found.");
                }
            }
            else
            {
                // Skapar en ny tom order om inget ID tillhandah�lls
                Order = new Order();
            }
            return Page();
        }



        // Metoden OnPostAsync() hanterar HTTP POST-f�rfr�gningar
        // fr�n best�llningsformul�ret.
        // Den loggar informationen om mottagen order och
        // f�rs�ker skapa ordern i det externa API:et.
        // Om modellen �r giltig, skickas best�llningen
        // till API:et och anv�ndaren omdirigeras till en annan sida.
        public async Task<IActionResult> OnPostAsync()
        {
            if (Order == null)
            {
                Order = new Order(); // S�kerst�ll att Order inte �r null
            }

            _logger.LogInformation($"Order received: {JsonConvert.SerializeObject(Order)}");

            // V�xlar spr�ket
            ViewData["Language"] = CurrentLanguage;

            // Kontrollerar om modellen �r giltig
            if (ModelState.IsValid)
            {
                _logger.LogInformation($"Order received: {JsonConvert.SerializeObject(Order)}");

                // Skapar ordern i det externa API:et
                await _orderService.CreateOrderInApi(Order);

                // Meddelande om framg�ng och omdirigering
                TempData["SuccessMessage"] = "Your order has been sent";
                return RedirectToPage("/LogisticsCenter/ShipmentCalculator");
            }
            else
            {
                // Om modellen �r ogiltig, logga valideringsfel och �terv�nd till sidan
                foreach (var entry in ModelState.Values)
                {
                    foreach (var error in entry.Errors)
                    {
                        // Logga eller hantera valideringsfel h�r
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