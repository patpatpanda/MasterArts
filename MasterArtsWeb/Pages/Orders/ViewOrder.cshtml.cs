//using MasterArtsLibrary.Services;
//using MasterArtsLibrary.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using System.ComponentModel.DataAnnotations;

//namespace MasterArtsWeb.Pages.Orders
//{
//    public class ViewOrderModel : BaseModel
//    {
//        private readonly OrderService _orderService;

//        [BindProperty]
//        [Required(ErrorMessage = "Please enter Order ID")]
//        public int OrderId { get; set; }

//        public OrderViewModel OrderViewModel { get; set; }

//        public ViewOrderModel(OrderService orderService, LanguageService languageService) : base(languageService)
//        {
//            _orderService = orderService;
//        }

//        public void OnGetView()
//        {
//            // Eventuell initial logik vid GET
//        }

//        public async Task<IActionResult> OnPostView()
//        {
//            if (ModelState.IsValid)
//            {
//                OrderViewModel = await _orderService.GetOrderViewModelAsync(OrderId);

//                if (OrderViewModel == null)
//                {
//                    ModelState.AddModelError(nameof(OrderId), "Order not found");
//                }
//            }

//            return Page();
//        }
//    }
//}
