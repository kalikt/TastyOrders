using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TastyOrders.Data.Models;
using TastyOrders.Data;
using TastyOrders.Web.ViewModels.Order;
using Microsoft.EntityFrameworkCore;

namespace TastyOrders.Web.Controllers
{
    using static Common.EntityValidationConstants.Order;
    public class OrderController : Controller
    {
        private readonly TastyOrdersDbContext context;  
        private readonly UserManager<ApplicationUser> userManager;

        public OrderController(TastyOrdersDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var orders = await context.Orders
                .Where(o => o.UserId == user.Id)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .Select(o => new OrderSummaryViewModel
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate.ToString(OrderDateFormat),
                    TotalAmount = o.TotalPrice,
                    Items = o.OrderItems.Select(oi => oi.MenuItem.Name).ToList()
                })
                .ToListAsync();

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

            // Retrieve user's cart
            var cart = await context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.MenuItem)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (cart == null || !cart.CartItems.Any())
            {
                TempData["ErrorMessage"] = "Your cart is empty!";
                return RedirectToAction("Index", "Cart");
            }

            // Create a new order
            var order = new Order
            {
                UserId = user.Id,
                OrderDate = DateTime.UtcNow,
                TotalPrice = cart.CartItems.Sum(ci => ci.Quantity * ci.MenuItem.Price),
                OrderItems = cart.CartItems.Select(ci => new OrderItem
                {
                    MenuItemId = ci.MenuItemId,
                    Quantity = ci.Quantity,
                    Price = ci.MenuItem.Price
                }).ToList()
            };

            context.Orders.Add(order);

            // Clear the cart
            context.CartItems.RemoveRange(cart.CartItems);
            await context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Your order has been placed successfully!";
            return RedirectToAction("Details", new { id = order.Id });
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var order = await context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .FirstOrDefaultAsync(o => o.Id == id && o.UserId == user.Id);

            if (order == null)
            {
                return NotFound();
            }

            var model = new OrderDetailsViewModel
            {
                Id = order.Id,
                OrderDate = order.OrderDate.ToString(OrderDateFormat),
                TotalAmount = order.TotalPrice,
                Items = order.OrderItems.Select(oi => new OrderItemViewModel
                {
                    Name = oi.MenuItem.Name,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            };

            return View(model);
        }
    }
}
