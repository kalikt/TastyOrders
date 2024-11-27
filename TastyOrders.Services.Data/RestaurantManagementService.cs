using Microsoft.EntityFrameworkCore;
using TastyOrders.Data;
using TastyOrders.Data.Models;
using TastyOrders.Services.Data.Interfaces;

namespace TastyOrders.Services.Data
{
    public class RestaurantManagementService : IRestaurantManagementService
    {
        private readonly TastyOrdersDbContext context;

        public RestaurantManagementService(TastyOrdersDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Restaurant>> GetAllRestaurantsAsync()
        {
            return await context.Restaurants.ToListAsync();
        }

        public async Task<bool> AddRestaurantAsync(string name, string location, string? imageUrl)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(location))
            {
                return false; 
            }

            var restaurant = new Restaurant
            {
                Name = name,
                Location = location,
                ImageUrl = imageUrl
            };

            context.Restaurants.Add(restaurant);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveRestaurantAsync(int restaurantId)
        {
            var restaurant = await context.Restaurants.FindAsync(restaurantId);
            if (restaurant == null)
            {
                return false; 
            }

            context.Restaurants.Remove(restaurant);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
