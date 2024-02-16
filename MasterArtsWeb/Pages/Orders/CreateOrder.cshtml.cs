

using MasterArtsLibrary.Models;
using MasterArtsLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MasterArtsWeb.Pages.Orders
{
    public class CreateOrderModel : PageModel
    {

        private readonly LanguageService _languageService;

        private readonly OrderService _orderService;
        [BindProperty]
        public Order Order { get; set; }
        public string CurrentLanguage { get; set; }

        


        public CreateOrderModel(MyDbContext dbContext, OrderService orderService, LanguageService languageServSender)
        {

            _orderService = orderService;
            _languageService = languageServSender;
        }

        public async Task OnGetAsync()
        {
            CurrentLanguage = _languageService.GetCurrentLanguage();
            ViewData["Language"] = CurrentLanguage;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            CurrentLanguage = _languageService.ToggleLanguage();
            ViewData["Language"] = CurrentLanguage;

            if (ModelState.IsValid)
            {

                // Skapa order i API
                await _orderService.CreateOrderInApi(Order);

                var recipientEmail = Order.Consignor.ConsignorEmail; 
                await _orderService.SendOrderConfirmationEmail(recipientEmail, Order);

                TempData["SuccessMessage"] = "Your order has been sent";
                return RedirectToPage("/Orders/CreateOrder");
            }

            return Page();
        }


    }


}

