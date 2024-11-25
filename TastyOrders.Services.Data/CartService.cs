using TastyOrders.Data.Models;
using TastyOrders.Data;
using TastyOrders.Services.Data.Interfaces;
using TastyOrders.Web.ViewModels.Cart;
using Microsoft.EntityFrameworkCore;

namespace TastyOrders.Services.Data
{
    public class CartService : ICartService
    {
        private readonly TastyOrdersDbContext context;

        public CartService(TastyOrdersDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddToCartAsync(string userId, int menuItemId)
        {
            var menuItem = await context.MenuItems
                .Include(mi => mi.Restaurant)
                .FirstOrDefaultAsync(mi => mi.Id == menuItemId);

            if (menuItem == null)
            {
                return false;
            }

            var cart = await context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.MenuItem)
                .ThenInclude(mi => mi.Restaurant)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>()
                };
                context.Carts.Add(cart);
            }
            else
            {
                var existingLocation = cart.CartItems
                    .Select(ci => ci.MenuItem.Restaurant.Location)
                    .FirstOrDefault();

                if (existingLocation != null && existingLocation != menuItem.Restaurant.Location)
                {
                    return false;
                }
            }

            var existingItem = cart.CartItems.FirstOrDefault(i => i.MenuItemId == menuItemId);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    MenuItemId = menuItem.Id,
                    Quantity = 1
                });
            }

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<CartViewModel> GetCartAsync(string userId)
        {
            var cart = await context.Carts
                .Where(c => c.UserId == userId)
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
                        .FirstOrDefault()
                })
                .FirstOrDefaultAsync();

            return cart ?? new CartViewModel();
        }

        public async Task<bool> UpdateQuantityAsync(int cartItemId, int quantity)
        {
            if (quantity < 1)
            {
                return false;
            }

            var cartItem = await context.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
            {
                return false;
            }

            cartItem.Quantity = quantity;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveItemAsync(int cartItemId)
        {
            var cartItem = await context.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
            {
                return false;
            }

            context.CartItems.Remove(cartItem);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
