using Microsoft.AspNetCore.Mvc;

namespace MasterArtsApi.Controllers
{
    public class OrderApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
