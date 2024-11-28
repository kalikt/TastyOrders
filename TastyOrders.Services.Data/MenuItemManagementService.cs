using Microsoft.EntityFrameworkCore;
using TastyOrders.Data;
using TastyOrders.Data.Models;
using TastyOrders.Services.Data.Interfaces;
using TastyOrders.Web.ViewModels.MenuItem;

namespace TastyOrders.Services.Data
{
    public class MenuItemManagementService : IMenuItemManagementService
    {
        private readonly TastyOrdersDbContext context;

        public MenuItemManagementService(TastyOrdersDbContext context)
        {
            this.context = context;
        }

        public async Task<Restaurant?> GetRestaurantWithMenuItemsAsync(int restaurantId)
        {
            return await context.Restaurants
                .Include(r => r.MenuItems)
                .FirstOrDefaultAsync(r => r.Id == restaurantId);
        }

        public async Task<bool> AddMenuItemAsync(int restaurantId, string name, decimal price, string description, string? imageUrl)
        {
            if (string.IsNullOrWhiteSpace(name) || price <= 0 || string.IsNullOrWhiteSpace(description))
            {
                return false;
            }

            var menuItem = new MenuItem
            {
                Name = name,
                Price = price,
                Description = description,
                ImageUrl = imageUrl,
                RestaurantId = restaurantId
            };

            context.MenuItems.Add(menuItem);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<int?> RemoveMenuItemAsync(int menuItemId)
        {
            var menuItem = await context.MenuItems.FindAsync(menuItemId);
            if (menuItem == null)
            {
                return null; 
            }

            var restaurantId = menuItem.RestaurantId;
            context.MenuItems.Remove(menuItem);
            await context.SaveChangesAsync();

            return restaurantId;
        }

        public async Task<EditMenuItemViewModel?> GetMenuItemByIdAsync(int menuItemId)
        {
            var menuItem = await context.MenuItems
        .Include(m => m.Restaurant)
        .FirstOrDefaultAsync(m => m.Id == menuItemId);

            if (menuItem == null) return null;

            return new EditMenuItemViewModel
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                Price = menuItem.Price,
                Description = menuItem.Description,
                ImageUrl = menuItem.ImageUrl ?? string.Empty,
                RestaurantId = menuItem.RestaurantId
            };
        }

        public async Task<bool> UpdateMenuItemAsync(EditMenuItemViewModel updatedMenuItem)
        {
            if (string.IsNullOrWhiteSpace(updatedMenuItem.Name) ||
               updatedMenuItem.Price <= 0 ||
               string.IsNullOrWhiteSpace(updatedMenuItem.Description))
            {
                return false; 
            }

            var menuItem = await context.MenuItems.FindAsync(updatedMenuItem.Id);
            if (menuItem == null)
            {
                return false; 
            }

            menuItem.Name = updatedMenuItem.Name;
            menuItem.Price = updatedMenuItem.Price;
            menuItem.Description = updatedMenuItem.Description;
            menuItem.ImageUrl = updatedMenuItem.ImageUrl;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<ManageMenuItemsViewModel?> GetManageMenuItemsViewModelAsync(int restaurantId)
        {
            var restaurant = await context.Restaurants
                .Include(r => r.MenuItems)
                .FirstOrDefaultAsync(r => r.Id == restaurantId);

            if (restaurant == null) return null;

            return new ManageMenuItemsViewModel
            {
                RestaurantId = restaurant.Id,
                RestaurantName = restaurant.Name,
                MenuItems = restaurant.MenuItems.Select(menuItem => new MenuItemViewModel
                {
                    Id = menuItem.Id,
                    Name = menuItem.Name,
                    Price = menuItem.Price,
                    Description = menuItem.Description
                })
            };
        }
    }
}