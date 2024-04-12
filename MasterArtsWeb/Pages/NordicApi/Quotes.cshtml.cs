using MasterArtsLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MasterArtsWeb.Pages.NordicApi
{
    public class QuotesModel : PageModel
    {
        private readonly MyDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public List<CustomerRates> CustomerRatesDetails { get; set; }
        public List<Order> CustomerOrders { get; set; }
        public string CustomerOrderNumber { get; set; }
        

        public QuotesModel(MyDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            CustomerOrderNumber = await GetCustomerNumberAsync(userId);
            
            if (string.IsNullOrWhiteSpace(CustomerOrderNumber))
            {
                return Page(); // Or handle the lack of a customer number appropriately
            }

            CustomerRatesDetails = await _context.CustomerRates
                                .Include(cr => cr.Totals)
                                .Include(cr => cr.Rates)
                                .Include(cr => cr.Sailing)
                                .Include(cr => cr.Agent)
                                .Include(cr => cr.ShippingRequest)
                                    .ThenInclude(sr => sr.Dimensions)  // This ensures Dimensions are included
                                .Where(cr => cr.CustomerOrderNumber == CustomerOrderNumber)
                                .ToListAsync();



            if (!CustomerRatesDetails.Any())
            {
                return NotFound($"No rates found for order number: {CustomerOrderNumber}");
            }

            return Page();
        }


        private async Task<string> GetCustomerNumberAsync(string userId)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.UserId == userId);

            return customer?.CustomerNumber;
        }


    }
}
