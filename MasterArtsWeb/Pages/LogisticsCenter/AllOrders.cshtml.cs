using MasterArtsLibrary.Models;
using MasterArtsWeb;
using MasterArtsWeb.Pages.LogisticsCenter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

public class AllOrdersModel : PageModel
{
    private readonly MyDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<ShipmentCalculatorModel> _logger;
    public AllOrdersModel(MyDbContext context, UserManager<IdentityUser> userManager, ILogger<ShipmentCalculatorModel> logger)
    {
        _context = context;
        _userManager = userManager;
        _logger = logger;

    }

    public List<Order> CustomerOrders { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user?.Email != "ops@artslogistics.se")
        {
            return Unauthorized();  // Bara tillåta åtkomst för specifika användare
        }

        try
        {
            CustomerOrders = await _context.Orders
                                    .Include(o => o.Goods) // Inkludera Goods i varje Order
                                    .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ett fel uppstod vid hämtning av orders: {ex.Message}");
            // Hantera exception här, kanske genom att visa ett felmeddelande till användaren
            return StatusCode(500, "Ett internt serverfel uppstod");
        }

        return Page();
    }

}
