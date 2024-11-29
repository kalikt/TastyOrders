using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TastyOrders.Data.Models;
using Microsoft.AspNetCore.Authorization;
using TastyOrders.Services.Data.Interfaces;

namespace TastyOrders.Web.Controllers
{
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
            if (!User.Identity.IsAuthenticated)
            {
                TempData["ErrorMessage"] = "You must be logged in to add items to the cart.";
                return Redirect("/Identity/Account/Register");
            }

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
        [Authorize]
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity)
        {
            var success = await cartService.UpdateQuantityAsync(cartItemId, quantity);

            if (!success)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize]
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
