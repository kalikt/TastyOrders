using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TastyOrders.Data.Models;
using TastyOrders.Data;
using Microsoft.EntityFrameworkCore;

namespace TastyOrders.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly TastyOrdersDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(TastyOrdersDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int menuItemId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            // Ensure the user's cart exists
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = user.Id,
                    CartItems = new List<CartItem>()
                };
                _context.Carts.Add(cart);
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
                var menuItem = await _context.MenuItems.FindAsync(menuItemId);
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

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Item added to cart!";
            return RedirectToAction("Index", "Menu");
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.MenuItem)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (cart == null || !cart.CartItems.Any())
            {
                return View("EmptyCart");
            }

            return View(cart);
        }

        // Update item quantity
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem == null || quantity < 1)
            {
                return BadRequest();
            }

            cartItem.Quantity = quantity;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Remove an item from the cart
        [HttpPost]
        public async Task<IActionResult> RemoveItem(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
            {
                return NotFound();
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
