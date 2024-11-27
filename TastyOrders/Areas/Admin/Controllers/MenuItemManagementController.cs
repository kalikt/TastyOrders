using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TastyOrders.Data.Models;
using TastyOrders.Services.Data.Interfaces;

namespace TastyOrders.Web.Areas.Admin.Controllers
{
    using static Common.ApplicationConstants;

    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]
    public class MenuItemManagementController : Controller
    {
        private readonly IMenuItemManagementService menuItemService;

        public MenuItemManagementController(IMenuItemManagementService menuItemService)
        {
            this.menuItemService = menuItemService;
        }

        [HttpGet]
        public async Task<IActionResult> ManageMenuItems(int restaurantId)
        {
            var restaurant = await menuItemService.GetRestaurantWithMenuItemsAsync(restaurantId);
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
            var success = await menuItemService.AddMenuItemAsync(restaurantId, name, price, description, imageUrl);

            if (!success)
            {
                TempData["ErrorMessage"] = "All fields are required, and price must be greater than 0.";
                return RedirectToAction(nameof(AddMenuItem), new { restaurantId });
            }

            TempData["SuccessMessage"] = "Menu item added successfully.";
            return RedirectToAction(nameof(ManageMenuItems), new { restaurantId });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveMenuItem(int menuItemId)
        {
            var restaurantId = await menuItemService.RemoveMenuItemAsync(menuItemId);

            if (restaurantId == null)
            {
                TempData["ErrorMessage"] = "Menu item not found.";
                return RedirectToAction("ManageRestaurants", "RestaurantManagement", new { area = AdminRoleName });
            }

            TempData["SuccessMessage"] = "Menu item removed successfully.";
            return RedirectToAction(nameof(ManageMenuItems), new { restaurantId });
        }

        [HttpGet]
        public async Task<IActionResult> EditMenuItem(int menuItemId)
        {
            var menuItem = await menuItemService.GetMenuItemByIdAsync(menuItemId);

            if (menuItem == null)
            {
                TempData["ErrorMessage"] = "Menu item not found.";
                return RedirectToAction("ManageRestaurants", "RestaurantManagement", new { area = "Admin" });
            }

            ViewBag.RestaurantName = menuItem.Restaurant.Name;
            return View(menuItem);
        }

        [HttpPost]
        public async Task<IActionResult> EditMenuItem(MenuItem updatedMenuItem)
        {
            var success = await menuItemService.UpdateMenuItemAsync(updatedMenuItem);

            if (!success)
            {
                TempData["ErrorMessage"] = "Menu item not found.";
                return RedirectToAction("ManageRestaurants", "RestaurantManagement", new { area = "Admin" });
            }

            TempData["SuccessMessage"] = "Menu item updated successfully.";
            return RedirectToAction("ManageMenuItems", new { restaurantId = updatedMenuItem.RestaurantId });
        }

    }
}
