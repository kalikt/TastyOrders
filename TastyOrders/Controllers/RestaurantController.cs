using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TastyOrders.Data;
using TastyOrders.Web.ViewModels.Restaurant;

namespace TastyOrders.Web.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly TastyOrdersDbContext context;

        public RestaurantController(TastyOrdersDbContext context)
        {
            this.context = context;
        }

        public IActionResult ChooseLocation()
        {
            var locations = context.Restaurants
                .Select(r => r.Location)
                .Distinct()
                .ToList();

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

            // Redirect to the restaurants listing page with the selected location as a query parameter
            return RedirectToAction("Index", new { location = model.SelectedLocation });
        }

        // GET: Restaurant
        public async Task<IActionResult> Index(string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                return RedirectToAction("ChooseLocation");
            }

            var restaurants = await context.Restaurants
                .Where(r => r.Location == location)
                .Select(r => new RestaurantIndexViewModel
            {
                Id = r.Id,
                Name = r.Name,
                Location = r.Location,
                ImageUrl = r.ImageUrl ?? string.Empty
            }).ToListAsync();

            var model = new RestaurantsWithLocationViewModel
            {
                SelectedLocation = location,
                Restaurants = restaurants
            };

            return View(model);
        }

        // GET: Restaurant/Menu/5
        public async Task<IActionResult> Menu(int id)
        {
            var restaurant = await context.Restaurants
            .Where(r => r.Id == id)
            .Select(r => new RestaurantMenuViewModel
            {
                Id = r.Id,
                Name = r.Name,
                Location = r.Location,
                MenuItems = r.MenuItems.Select(m => new MenuItemViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    ImageUrl = m.ImageUrl ?? string.Empty,
                    Price = m.Price
                }).ToList()
            })
            .FirstOrDefaultAsync();

            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

    }
}
