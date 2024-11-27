using TastyOrders.Data.Models;

namespace TastyOrders.Services.Data.Interfaces
{
    public interface IRestaurantManagementService
    {
        Task<List<Restaurant>> GetAllRestaurantsAsync();
        Task<bool> AddRestaurantAsync(string name, string location, string? imageUrl);
        Task<bool> RemoveRestaurantAsync(int restaurantId);
    }
}
