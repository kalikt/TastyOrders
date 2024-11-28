using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TastyOrders.Data;
using TastyOrders.Services.Data.Interfaces;
using TastyOrders.Web.ViewModels.Review;

namespace TastyOrders.Web.Areas.Admin.Controllers
{
    using static Common.ApplicationConstants;

    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]
    public class ReviewManagementController : Controller
    {
        private readonly IReviewManagementService reviewService;

        public ReviewManagementController(IReviewManagementService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> ManageReviews()
        {
            var reviews = await reviewService.GetAllReviewsAsync();
            var model = new ManageReviewsViewModel
            {
                Reviews = reviews
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var success = await reviewService.DeleteReviewAsync(reviewId);

            if (!success)
            {
                TempData["ErrorMessage"] = "Review not found.";
                return RedirectToAction(nameof(ManageReviews));
            }

            TempData["SuccessMessage"] = "Review deleted successfully.";
            return RedirectToAction(nameof(ManageReviews));
        }
    }
}
