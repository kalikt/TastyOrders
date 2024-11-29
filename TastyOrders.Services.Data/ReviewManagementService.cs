using Microsoft.EntityFrameworkCore;
using TastyOrders.Data;
using TastyOrders.Services.Data.Interfaces;
using TastyOrders.Web.ViewModels.Review;

namespace TastyOrders.Services.Data
{
    public class ReviewManagementService : IReviewManagementService
    {
        private readonly TastyOrdersDbContext context;

        public ReviewManagementService(TastyOrdersDbContext context)
        {
            this.context = context;
        }

        public async Task<List<ReviewIndexViewModel>> GetAllReviewsAsync()
        {
            return await context.Reviews
                .Include(r => r.Restaurant)
                .Include(r => r.User)
                .Select(r => new ReviewIndexViewModel
                {
                    Id = r.Id,
                    RestaurantName = r.Restaurant.Name,
                    UserName = r.User.UserName ?? string.Empty,
                    Rating = r.Rating,
                    Comment = r.Comment
                })
                .ToListAsync();
        }

        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            var review = await context.Reviews.FindAsync(reviewId);
            if (review == null)
            {
                return false; 
            }

            context.Reviews.Remove(review);
            await context.SaveChangesAsync();
            return true; 
        }
    }
}
