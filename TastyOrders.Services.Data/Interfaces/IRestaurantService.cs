using TastyOrders.Web.ViewModels.Restaurant;

namespace TastyOrders.Services.Data.Interfaces
{
    public interface IRestaurantService
    {
        Task<List<string>> GetDistinctLocationsAsync();
        Task<List<RestaurantIndexViewModel>> GetRestaurantsByLocationAsync(string location, string? search);
        Task<RestaurantDetailsViewModel?> GetRestaurantDetailsAsync(int id);
    }
}
