using TastyOrders.Web.ViewModels.Restaurant;

namespace TastyOrders.Services.Data.Interfaces
{
    public interface IMenuService
    {
        Task<RestaurantMenuViewModel?> GetRestaurantMenuAsync(int restaurantId);
    }
}
