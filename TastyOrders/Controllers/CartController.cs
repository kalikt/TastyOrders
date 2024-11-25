﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TastyOrders.Data.Models;
using TastyOrders.Data;
using Microsoft.EntityFrameworkCore;
using TastyOrders.Web.ViewModels.Cart;
using Microsoft.AspNetCore.Authorization;

namespace TastyOrders.Web.Controllers
{
    [Authorize]
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

            // Retrieve the menu item and its restaurant
            var menuItem = await context.MenuItems
                .Include(mi => mi.Restaurant)
                .FirstOrDefaultAsync(mi => mi.Id == menuItemId);

            if (menuItem == null)
            {
                return NotFound();
            }

            // Ensure the user's cart exists
            var cart = await context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.MenuItem)
                .ThenInclude(mi => mi.Restaurant)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (cart == null)
            {
                // Create a new cart if none exists
                cart = new Cart
                {
                    UserId = user.Id,
                    CartItems = new List<CartItem>()
                };
                context.Carts.Add(cart);
            }
            else
            {
                // Check if cart already contains items from a different location
                var existingLocation = cart.CartItems
                    .Select(ci => ci.MenuItem.Restaurant.Location)
                    .FirstOrDefault();

                if (existingLocation != null && existingLocation != menuItem.Restaurant.Location)
                {
                    TempData["ErrorMessage"] = $"You can only add items from the same location ({existingLocation}).";
                    return RedirectToAction("Index", "Cart");
                }
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
        .Include(c => c.CartItems)
        .ThenInclude(ci => ci.MenuItem)
        .ThenInclude(mi => mi.Restaurant)
        .Select(c => new CartViewModel
        {
            Items = c.CartItems.Select(i => new CartItemViewModel
            {
                Id = i.Id,
                Name = i.MenuItem.Name,
                Price = i.MenuItem.Price,
                Quantity = i.Quantity
            }).ToList(),
            SelectedLocation = c.CartItems
                .Select(ci => ci.MenuItem.Restaurant.Location) 
                .FirstOrDefault() // All items should share the same location
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
