using MasterArtsWeb.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using MasterArtsLibrary.Models;
using Microsoft.EntityFrameworkCore;
using MasterArtsWeb;
using Microsoft.AspNetCore.Identity;

public class MyOrdersModel : PageModel
{
    private readonly MyDbContext _context;
    public List<Order> CustomerOrders { get; set; }
   
    public string CustomerNumber { get; set; }
    private readonly UserManager<IdentityUser> _userManager;
    public Order Order { get; set; } = new Order();
    
    public MyOrdersModel(MyDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // Metoden OnGetAsync hämtar användarens kundnummer och
    // visar tidigare beställningar om kundnummer är tillgängligt.
    // Om kundnummer saknas, returneras sidan för att hantera situationen.
    public async Task<IActionResult> OnGetAsync()
    {
        var userId = _userManager.GetUserId(User); // Hämtar användarens ID
        CustomerNumber = await GetCustomerNumberAsync(userId); 
        // Hämtar kundnummer med hjälp av användarens ID

        // Hanterar situationer där kundnummer saknas
        if (string.IsNullOrWhiteSpace(CustomerNumber))
        {
            return Page(); // Returnerar sidan
        }

        // Hämtar kundens tidigare beställningar från databasen
        CustomerOrders = await _context.Orders
                                .Where(o => o.Customer == CustomerNumber)
                                // Hämtar beställningar för specifik kund
                                .Include(o => o.Goods) // Inkluderar varor i varje order
                                .ToListAsync(); // Konverterar till lista

        return Page(); // Returnerar sidan
    }

   
    // Metoden GetCustomerNumberAsync hämtar kundnummer baserat på användar-ID
    public async Task<string> GetCustomerNumberAsync(string userId)
    {
        // Hämtar kundens information från databasen baserat på användar-ID
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.UserId == userId);

        return customer?.CustomerNumber; // Returnerar kundnummer om det finns, annars null
    }

}
