using TastyOrders.Data.Models;

namespace TastyOrders.Services.Data.Interfaces
{
    public interface IMenuItemManagementService
    {
        Task<Restaurant?> GetRestaurantWithMenuItemsAsync(int restaurantId);
        Task<bool> AddMenuItemAsync(int restaurantId, string name, decimal price, string description, string? imageUrl);
        Task<int?> RemoveMenuItemAsync(int menuItemId);
        Task<MenuItem?> GetMenuItemByIdAsync(int menuItemId);
        Task<bool> UpdateMenuItemAsync(MenuItem updatedMenuItem);
    }
}
