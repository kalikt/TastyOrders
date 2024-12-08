using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TastyOrders.Data.Models;
using Microsoft.AspNetCore.Authorization;
using TastyOrders.Services.Data.Interfaces;

namespace TastyOrders.Web.Controllers
{
    using static Common.ApplicationConstants;
    using static Common.ErrorMessages.Order;

    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly UserManager<ApplicationUser> userManager;

        public OrderController(IOrderService orderService,
            UserManager<ApplicationUser> userManager)
        {
            this.orderService = orderService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var orders = await orderService.GetUserOrdersAsync(user.Id);
            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var orderId = await orderService.PlaceOrderAsync(user.Id);

            if (orderId == null)
            {
                TempData[ErrorMessage] = EmptyCartMessage;
                return RedirectToAction(nameof(Index), nameof(Cart));
            }

            TempData[SuccessMessage] = OrderSuccessMessage;
            return RedirectToAction(nameof(Details), new { id = orderId });
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var orderDetails = await orderService.GetOrderDetailsAsync(id, user.Id);

            if (orderDetails == null)
            {
                return NotFound();
            }

            return View(orderDetails);
        }
    }
}
