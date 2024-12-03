using TastyOrders.Data.Models;
using TastyOrders.Web.ViewModels.Restaurant;

namespace TastyOrders.Services.Data.Interfaces
{
    public interface IRestaurantManagementService
    {
        Task<List<RestaurantViewModel>> GetAllRestaurantsAsync();
        Task<bool> AddRestaurantAsync(string name, string location, string? imageUrl);
        Task<bool> RemoveRestaurantAsync(int restaurantId);

        public Task<EditRestaurantViewModel?> GetRestaurantByIdAsync(int id);
        Task<bool> EditRestaurantAsync(EditRestaurantViewModel editedModel);
    }
}
