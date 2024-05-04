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

    // Metoden OnGetAsync h�mtar anv�ndarens kundnummer och
    // visar tidigare best�llningar om kundnummer �r tillg�ngligt.
    // Om kundnummer saknas, returneras sidan f�r att hantera situationen.
    public async Task<IActionResult> OnGetAsync()
    {
        var userId = _userManager.GetUserId(User); // H�mtar anv�ndarens ID
        CustomerNumber = await GetCustomerNumberAsync(userId); 
        // H�mtar kundnummer med hj�lp av anv�ndarens ID

        // Hanterar situationer d�r kundnummer saknas
        if (string.IsNullOrWhiteSpace(CustomerNumber))
        {
            return Page(); // Returnerar sidan
        }

        // H�mtar kundens tidigare best�llningar fr�n databasen
        CustomerOrders = await _context.Orders
                                .Where(o => o.Customer == CustomerNumber)
                                // H�mtar best�llningar f�r specifik kund
                                .Include(o => o.Goods) // Inkluderar varor i varje order
                                .ToListAsync(); // Konverterar till lista

        return Page(); // Returnerar sidan
    }

   
    // Metoden GetCustomerNumberAsync h�mtar kundnummer baserat p� anv�ndar-ID
    public async Task<string> GetCustomerNumberAsync(string userId)
    {
        // H�mtar kundens information fr�n databasen baserat p� anv�ndar-ID
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.UserId == userId);

        return customer?.CustomerNumber; // Returnerar kundnummer om det finns, annars null
    }

}
