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
    public List<Goods> CustomerGoods { get; set; }
    public string CustomerNumber { get; set; }
    private readonly UserManager<IdentityUser> _userManager;
    public Order Order { get; set; } = new Order();
    
    public MyOrdersModel(MyDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var userId = _userManager.GetUserId(User);
        CustomerNumber = await GetCustomerNumberAsync(userId);
        Order.Customer = CustomerNumber;
        if (string.IsNullOrWhiteSpace(CustomerNumber))
        {
            return Page(); // Or handle the lack of a customer number appropriately
        }

        CustomerOrders = await _context.Orders
                                .Where(o => o.Customer == CustomerNumber)
                                .Include(o => o.Goods) // Inkludera Goods i varje Order
                                .ToListAsync();

        return Page();
    }

    public async Task<string> GetCustomerNumberAsync(string userId)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.UserId == userId);

        return customer?.CustomerNumber;
    }
}
