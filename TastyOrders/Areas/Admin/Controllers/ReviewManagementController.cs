using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TastyOrders.Data;

namespace TastyOrders.Web.Areas.Admin.Controllers
{
    using static Common.ApplicationConstants;

    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]
    public class ReviewManagementController : Controller
    {
        private readonly TastyOrdersDbContext context;

        public ReviewManagementController(TastyOrdersDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ManageReviews()
        {
            var reviews = await context.Reviews
                .Include(r => r.Restaurant)
                .Include(r => r.User)
                .ToListAsync();

            return View(reviews);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var review = await context.Reviews.FindAsync(reviewId);
            if (review == null)
            {
                TempData["ErrorMessage"] = "Review not found.";
                return RedirectToAction(nameof(ManageReviews));
            }

            context.Reviews.Remove(review);
            await context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Review deleted successfully.";
            return RedirectToAction(nameof(ManageReviews));
        }
    }
}
