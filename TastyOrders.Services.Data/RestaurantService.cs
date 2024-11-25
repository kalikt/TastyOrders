using Microsoft.EntityFrameworkCore;
using TastyOrders.Data;
using TastyOrders.Services.Data.Interfaces;
using TastyOrders.Web.ViewModels.Restaurant;
using TastyOrders.Web.ViewModels.Review;

namespace TastyOrders.Services.Data
{
    public class RestaurantService : IRestaurantService
    {
        private readonly TastyOrdersDbContext context;

        public RestaurantService(TastyOrdersDbContext context)
        {
            this.context = context;
        }

        public async Task<List<string>> GetDistinctLocationsAsync()
        {
            return await context.Restaurants
                .Select(r => r.Location)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<RestaurantIndexViewModel>> GetRestaurantsByLocationAsync(string location)
        {
            return await context.Restaurants
                .Where(r => r.Location == location)
                .Select(r => new RestaurantIndexViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Location = r.Location,
                    ImageUrl = r.ImageUrl ?? string.Empty
                }).ToListAsync();
        }

        public async Task<RestaurantDetailsViewModel?> GetRestaurantDetailsAsync(int id)
        {
            var restaurant = await context.Restaurants
                .Include(r => r.Reviews)
                .ThenInclude(review => review.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurant == null)
            {
                return null;
            }

            return new RestaurantDetailsViewModel
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Location = restaurant.Location,
                ImageUrl = restaurant.ImageUrl ?? string.Empty,
                Reviews = restaurant.Reviews.Select(r => new ReviewViewModel
                {
                    Rating = r.Rating,
                    Comment = r.Comment,
                    UserName = r.User.UserName ?? string.Empty,
                    CreatedAt = r.CreatedAt.ToString(Common.EntityValidationConstants.Review.CreatedAtDateFormat)
                })
                .ToList()
            };
        }
    }
}
