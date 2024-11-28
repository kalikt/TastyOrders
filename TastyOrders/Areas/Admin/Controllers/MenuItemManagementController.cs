using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TastyOrders.Data.Models;
using TastyOrders.Services.Data.Interfaces;
using TastyOrders.Web.ViewModels.MenuItem;

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
            var model = await menuItemService.GetManageMenuItemsViewModelAsync(restaurantId);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Restaurant not found.";
                return RedirectToAction("ManageRestaurants", "RestaurantManagement", new { area = AdminRoleName });
            }

            return View(model);
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
            var model = await menuItemService.GetMenuItemByIdAsync(menuItemId);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Menu item not found.";
                return RedirectToAction("ManageRestaurants", "RestaurantManagement", new { area = AdminRoleName });
            }

            ViewBag.RestaurantName = (await menuItemService.GetRestaurantWithMenuItemsAsync(model.RestaurantId))?.Name;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditMenuItem(EditMenuItemViewModel updatedMenuItem)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RestaurantName = (await menuItemService.GetRestaurantWithMenuItemsAsync(updatedMenuItem.RestaurantId))?.Name;
                return View(updatedMenuItem);
            }

            var success = await menuItemService.UpdateMenuItemAsync(updatedMenuItem);
            if (!success)
            {
                TempData["ErrorMessage"] = "Failed to update the menu item. Please try again.";
                return RedirectToAction("ManageMenuItems", new { restaurantId = updatedMenuItem.RestaurantId });
            }

            TempData["SuccessMessage"] = "Menu item updated successfully.";
            return RedirectToAction("ManageMenuItems", new { restaurantId = updatedMenuItem.RestaurantId });
        }

    }
}
