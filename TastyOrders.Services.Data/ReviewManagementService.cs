using Microsoft.EntityFrameworkCore;
using TastyOrders.Data;
using TastyOrders.Data.Models;
using TastyOrders.Services.Data.Interfaces;

namespace TastyOrders.Services.Data
{
    public class ReviewManagementService : IReviewManagementService
    {
        private readonly TastyOrdersDbContext context;

        public ReviewManagementService(TastyOrdersDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Review>> GetAllReviewsAsync()
        {
            return await context.Reviews
                .Include(r => r.Restaurant)
                .Include(r => r.User)
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
