using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TastyOrders.Data;
using TastyOrders.Data.Models;

namespace TastyOrders.Web.Areas.Admin.Controllers
{
    using static Common.ApplicationConstants;

    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]
    public class MenuItemManagementController : Controller
    {
        private readonly TastyOrdersDbContext context;

        public MenuItemManagementController(TastyOrdersDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ManageMenuItems(int restaurantId)
        {
            var restaurant = await context.Restaurants
                .Include(r => r.MenuItems)
                .FirstOrDefaultAsync(r => r.Id == restaurantId);

            if (restaurant == null)
            {
                TempData["ErrorMessage"] = "Restaurant not found.";
                return RedirectToAction("ManageRestaurants", "RestaurantManagement", new { area = AdminRoleName });
            }

            ViewBag.RestaurantName = restaurant.Name;
            ViewBag.RestaurantId = restaurant.Id;

            return View(restaurant.MenuItems);
        }

        [HttpGet]
        public IActionResult AddMenuItem(int restaurantId)
        {
            ViewBag.RestaurantId = restaurantId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMenuItem(int restaurantId, string name, decimal price, string description, string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(name) || price <= 0 || string.IsNullOrWhiteSpace(description))
            {
                TempData["ErrorMessage"] = "All fields are required, and price must be greater than 0.";
                return RedirectToAction(nameof(AddMenuItem), new { restaurantId });
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

            TempData["SuccessMessage"] = "Menu item added successfully.";
            return RedirectToAction(nameof(ManageMenuItems), new { restaurantId });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveMenuItem(int menuItemId)
        {
            var menuItem = await context.MenuItems.FindAsync(menuItemId);
            if (menuItem == null)
            {
                TempData["ErrorMessage"] = "Menu item not found.";
                return RedirectToAction(nameof(ManageMenuItems), new { restaurantId = menuItem.RestaurantId });
            }

            context.MenuItems.Remove(menuItem);
            await context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Menu item removed successfully.";
            return RedirectToAction(nameof(ManageMenuItems), new { restaurantId = menuItem.RestaurantId });
        }

        [HttpGet]
        public async Task<IActionResult> EditMenuItem(int menuItemId)
        {
            var menuItem = await context.MenuItems
                .Include(m => m.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == menuItemId);

            if (menuItem == null)
            {
                TempData["ErrorMessage"] = "Menu item not found.";
                return RedirectToAction("ManageRestaurants", "RestaurantManagement", new { area = "Admin" });
            }

            var model = new MenuItem
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                Price = menuItem.Price,
                Description = menuItem.Description,
                ImageUrl = menuItem.ImageUrl,
                RestaurantId = menuItem.RestaurantId
            };

            ViewBag.RestaurantName = menuItem.Restaurant.Name;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditMenuItem(MenuItem updatedMenuItem)
        {
            var menuItem = await context.MenuItems
                .FirstOrDefaultAsync(m => m.Id == updatedMenuItem.Id);

            if (menuItem == null)
            {
                TempData["ErrorMessage"] = "Menu item not found.";
                return RedirectToAction("ManageRestaurants", "RestaurantManagement", new { area = "Admin" });
            }

            menuItem.Name = updatedMenuItem.Name;
            menuItem.Price = updatedMenuItem.Price;
            menuItem.Description = updatedMenuItem.Description;
            menuItem.ImageUrl = updatedMenuItem.ImageUrl;

            await context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Menu item updated successfully.";
            return RedirectToAction("ManageMenuItems", new { restaurantId = menuItem.RestaurantId });
        }

    }
}
