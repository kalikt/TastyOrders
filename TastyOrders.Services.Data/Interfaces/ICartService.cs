using TastyOrders.Web.ViewModels.Cart;

namespace TastyOrders.Services.Data.Interfaces
{
    public interface ICartService
    {
        Task<bool> AddToCartAsync(string userId, int menuItemId);
        Task<CartViewModel> GetCartAsync(string userId);
        Task<bool> UpdateQuantityAsync(int cartItemId, int quantity);
        Task<bool> RemoveItemAsync(int cartItemId);
    }
}
