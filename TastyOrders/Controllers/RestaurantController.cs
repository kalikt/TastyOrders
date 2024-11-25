using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TastyOrders.Data;
using TastyOrders.Services.Data.Interfaces;
using TastyOrders.Web.ViewModels.Restaurant;
using TastyOrders.Web.ViewModels.Review;

namespace TastyOrders.Web.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
        }

        public async Task<IActionResult> ChooseLocation()
        {
            var locations = await restaurantService.GetDistinctLocationsAsync();

            var model = new LocationSelectionViewModel
            {
                Locations = locations
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult ChooseLocation(LocationSelectionViewModel model)
        {
            if (string.IsNullOrEmpty(model.SelectedLocation))
            {
                ModelState.AddModelError("", "Please select a location.");
                return View(model);
            }
                
            TempData["SelectedLocation"] = model.SelectedLocation;

            return RedirectToAction("Index", new { location = model.SelectedLocation });
        }

        [HttpGet]
        public async Task<IActionResult> Index(string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                return RedirectToAction(nameof(ChooseLocation));
            }

            var restaurants = await restaurantService.GetRestaurantsByLocationAsync(location);

            var model = new RestaurantsWithLocationViewModel
            {
                SelectedLocation = location,
                Restaurants = restaurants
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await restaurantService.GetRestaurantDetailsAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

    }
}
