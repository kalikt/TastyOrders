using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TastyOrders.Services.Data.Interfaces;
using TastyOrders.Web.ViewModels.Restaurant;

namespace TastyOrders.Web.Areas.Admin.Controllers
{
    using static Common.ApplicationConstants;
    using static Common.ErrorMessages.RestaurantManagement;
    using static Common.EntityValidationMessages.RestaurantManagement;

    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]
    public class RestaurantManagementController : Controller
    {
        private readonly IRestaurantManagementService restaurantService;
        public RestaurantManagementController(IRestaurantManagementService restaurantService)
        {
            this.restaurantService = restaurantService;
        }


        [HttpGet]
        public async Task<IActionResult> ManageRestaurants()
        {
            var restaurants = await restaurantService.GetAllRestaurantsAsync();
            var model = new ManageRestaurantsViewModel
            {
                Restaurants = restaurants
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult AddRestaurant()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRestaurant(string name, string location, string imageUrl)
        {
            var success = await restaurantService.AddRestaurantAsync(name, location, imageUrl);

            if (!success)
            {
                TempData[ErrorMessage] = NameLocationRequired;
                return RedirectToAction(nameof(ManageRestaurants));
            }

            TempData[SuccessMessage] = $"Restaurant '{name}' has been added successfully.";
            return RedirectToAction(nameof(ManageRestaurants));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRestaurant(int restaurantId)
        {
            var success = await restaurantService.RemoveRestaurantAsync(restaurantId);

            if (!success)
            {
                TempData[ErrorMessage] = RestaurantNotFoundMessage;
                return RedirectToAction(nameof(ManageRestaurants));
            }

            TempData[SuccessMessage] = RestaurantSuccessfullyRemovedMessage;
            return RedirectToAction(nameof(ManageRestaurants));
        }
    }
}
