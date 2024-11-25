using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TastyOrders.Data.Models;
using TastyOrders.Data;
using Microsoft.EntityFrameworkCore;
using TastyOrders.Web.ViewModels.Cart;
using Microsoft.AspNetCore.Authorization;
using TastyOrders.Services.Data.Interfaces;

namespace TastyOrders.Web.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        private readonly UserManager<ApplicationUser> userManager;

        public CartController(ICartService cartService,
            UserManager<ApplicationUser> userManager)
        {
            this.cartService = cartService;
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

            var success = await cartService.AddToCartAsync(user.Id, menuItemId);

            if (!success)
            {
                TempData["ErrorMessage"] = "Unable to add item to cart. Ensure all items are from the same location.";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Item added to cart!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var cart = await cartService.GetCartAsync(user.Id);
            return View(cart);
        }

        // Update item quantity
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity)
        {
            var success = await cartService.UpdateQuantityAsync(cartItemId, quantity);

            if (!success)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }

        // Remove an item from the cart
        [HttpPost]
        public async Task<IActionResult> RemoveItem(int cartItemId)
        {
            var success = await cartService.RemoveItemAsync(cartItemId);

            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
