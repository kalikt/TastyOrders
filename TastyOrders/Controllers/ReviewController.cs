using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TastyOrders.Data;
using TastyOrders.Data.Models;
using TastyOrders.Services.Data.Interfaces;
using TastyOrders.Web.ViewModels.Review;

namespace TastyOrders.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService reviewService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReviewController(IReviewService reviewService, UserManager<ApplicationUser> userManager)
        {
            this.reviewService = reviewService;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create(int restaurantId)
        {
            var model = await reviewService.GetReviewCreateModelAsync(restaurantId);

            if (model == null)
            {
                return NotFound();
            }

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
            if (user == null)
            {
                return Unauthorized();
            }

            await reviewService.AddReviewAsync(model, user.Id);

            return RedirectToAction("Details", "Restaurant", new { id = model.RestaurantId });
        }

        [HttpGet]
        public async Task<IActionResult> List(int restaurantId)
        {
            var reviews = await reviewService.GetReviewsByRestaurantAsync(restaurantId);
            return View(reviews);
        }

        [HttpGet]
        public async Task<IActionResult> MyReviews()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var reviews = await reviewService.GetUserReviewsAsync(user.Id);
            return View(reviews);
        }
    }
}
