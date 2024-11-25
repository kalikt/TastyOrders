using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TastyOrders.Data;
using TastyOrders.Data.Models;
using TastyOrders.Web.ViewModels.Review;

namespace TastyOrders.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly TastyOrdersDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public ReviewController(TastyOrdersDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create(int restaurantId)
        {
            var restaurant = await context.Restaurants.FindAsync(restaurantId);
            if (restaurant == null)
            {
                return NotFound();
            }

            var model = new ReviewCreateViewModel
            {
                RestaurantId = restaurant.Id,
                RestaurantName = restaurant.Name
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ReviewCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.GetUserAsync(User);

            var review = new Review
            {
                RestaurantId = model.RestaurantId,
                UserId = user.Id,
                Rating = model.Rating,
                Comment = model.Comment
            };

            context.Reviews.Add(review);
            await context.SaveChangesAsync();

            return RedirectToAction("Details", "Restaurant", new { id = model.RestaurantId });
        }

        [HttpGet]
        public async Task<IActionResult> List(int restaurantId)
        {
            var reviews = await context.Reviews
                .Where(r => r.RestaurantId == restaurantId)
                .Include(r => r.User)
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new ReviewCreateViewModel
                {
                    RestaurantName = r.Restaurant.Name,
                    Rating = r.Rating,
                    Comment = r.Comment
                })
                .ToListAsync();

            return View(reviews);
        }
    }
}
