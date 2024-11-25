using Microsoft.EntityFrameworkCore;
using TastyOrders.Data;
using TastyOrders.Data.Models;
using TastyOrders.Services.Data.Interfaces;
using TastyOrders.Web.ViewModels.Review;

namespace TastyOrders.Services.Data
{
    using static Common.EntityValidationConstants.Review;
    public class ReviewService : IReviewService
    {
        private readonly TastyOrdersDbContext context;

        public ReviewService(TastyOrdersDbContext context)
        {
            this.context = context;
        }

        public async Task<ReviewCreateViewModel?> GetReviewCreateModelAsync(int restaurantId)
        {
            var restaurant = await context.Restaurants.FindAsync(restaurantId);

            if (restaurant == null)
            {
                return null;
            }

            return new ReviewCreateViewModel
            {
                RestaurantId = restaurant.Id,
                RestaurantName = restaurant.Name
            };
        }

        public async Task<bool> AddReviewAsync(ReviewCreateViewModel model, string userId)
        {
            var review = new Review
            {
                RestaurantId = model.RestaurantId,
                UserId = userId,
                Rating = model.Rating,
                Comment = model.Comment
            };

            context.Reviews.Add(review);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ReviewCreateViewModel>> GetReviewsByRestaurantAsync(int restaurantId)
        {
            return await context.Reviews
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
        }

        public async Task<List<MyReviewsViewModel>> GetUserReviewsAsync(string userId)
        {
            return await context.Reviews
                .Where(r => r.UserId == userId)
                .Include(r => r.Restaurant)
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new MyReviewsViewModel
                {
                    RestaurantName = r.Restaurant.Name,
                    RestaurantId = r.Restaurant.Id,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt.ToString(CreatedAtDateFormat)
                })
                .ToListAsync();
        }
    }
}
