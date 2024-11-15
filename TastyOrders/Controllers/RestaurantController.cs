using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TastyOrders.Data;

namespace TastyOrders.Web.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly TastyOrdersDbContext _context;

        public RestaurantController(TastyOrdersDbContext context)
        {
            _context = context;
        }

        // GET: Restaurant
        public async Task<IActionResult> Index()
        {
            var restaurants = await _context.Restaurants.ToListAsync();
            return View(restaurants);
        }

        // GET: Restaurant/Menu/5
        public async Task<IActionResult> Menu(int id)
        {
            var restaurant = await _context.Restaurants
                .Include(r => r.MenuItems)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

    }
}
