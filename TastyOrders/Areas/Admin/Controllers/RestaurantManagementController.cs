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
    public class RestaurantManagementController : Controller
    {
        private readonly TastyOrdersDbContext context;

        public RestaurantManagementController(TastyOrdersDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ManageRestaurants()
        {
            var restaurants = await context.Restaurants.ToListAsync();
            return View(restaurants);
        }

        [HttpGet]
        public IActionResult AddRestaurant()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRestaurant(string name, string location, string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(location))
            {
                TempData["ErrorMessage"] = "Name and location are required.";
                return RedirectToAction(nameof(ManageRestaurants));
            }

            var restaurant = new Restaurant
            {
                Name = name,
                Location = location,
                ImageUrl = imageUrl
            };

            context.Restaurants.Add(restaurant);
            await context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Restaurant '{name}' has been added successfully.";
            return RedirectToAction(nameof(ManageRestaurants));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRestaurant(int restaurantId)
        {
            var restaurant = await context.Restaurants.FindAsync(restaurantId);
            if (restaurant == null)
            {
                TempData["ErrorMessage"] = "Restaurant not found.";
                return RedirectToAction(nameof(ManageRestaurants));
            }

            context.Restaurants.Remove(restaurant);
            await context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Restaurant '{restaurant.Name}' has been removed successfully.";
            return RedirectToAction(nameof(ManageRestaurants));
        }
    }
}
