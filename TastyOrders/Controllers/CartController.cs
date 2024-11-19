using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TastyOrders.Data.Models;
using TastyOrders.Data;
using Microsoft.EntityFrameworkCore;
using TastyOrders.Web.ViewModels.Cart;

namespace TastyOrders.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly TastyOrdersDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public CartController(TastyOrdersDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int menuItemId)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            // Ensure the user's cart exists
            var cart = await context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = user.Id,
                    CartItems = new List<CartItem>()
                };
                context.Carts.Add(cart);
            }

            // Check if the item is already in the cart
            var existingItem = cart.CartItems.FirstOrDefault(i => i.MenuItemId == menuItemId);
            if (existingItem != null)
            {
                // Increment the quantity
                existingItem.Quantity++;
            }
            else
            {
                // Add the item to the cart
                var menuItem = await context.MenuItems.FindAsync(menuItemId);
                if (menuItem == null)
                {
                    return NotFound();
                }

                var cartItem = new CartItem
                {
                    MenuItemId = menuItem.Id,
                    Quantity = 1
                };
                cart.CartItems.Add(cartItem);
            }

            await context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Item added to cart!";
            return RedirectToAction("Index", "Cart");
        }
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var cart = await context.Carts
            .Where(c => c.UserId == user.Id) 
            .Select(c => new CartViewModel
            {
                Items = c.CartItems.Select(i => new CartItemViewModel
                {
                    Id = i.Id,
                    Name = i.MenuItem.Name,
                    Price = i.MenuItem.Price,
                    Quantity = i.Quantity
                }).ToList()
            })
            .FirstOrDefaultAsync();

            if (cart == null)
            {
                cart = new CartViewModel(); 
            }

            return View(cart);
        }

        // Update item quantity
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity)
        {
            var cartItem = await context.CartItems.FindAsync(cartItemId);
            if (cartItem == null || quantity < 1)
            {
                return BadRequest();
            }

            cartItem.Quantity = quantity;
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Remove an item from the cart
        [HttpPost]
        public async Task<IActionResult> RemoveItem(int cartItemId)
        {
            var cartItem = await context.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
            {
                return NotFound();
            }

            context.CartItems.Remove(cartItem);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
