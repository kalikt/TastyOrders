using TastyOrders.Data.Models;
using TastyOrders.Web.ViewModels.MenuItem;

namespace TastyOrders.Services.Data.Interfaces
{
    public interface IMenuItemManagementService
    {
        Task<Restaurant?> GetRestaurantWithMenuItemsAsync(int restaurantId);
        Task<bool> AddMenuItemAsync(int restaurantId, string name, decimal price, string description, string? imageUrl);
        Task<int?> RemoveMenuItemAsync(int menuItemId);
        Task<EditMenuItemViewModel?> GetMenuItemByIdAsync(int menuItemId);
        Task<bool> UpdateMenuItemAsync(EditMenuItemViewModel updatedMenuItem);

        public Task<ManageMenuItemsViewModel?> GetManageMenuItemsViewModelAsync(int restaurantId);
    }
}
