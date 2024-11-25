using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyOrders.Web.ViewModels.Restaurant;

namespace TastyOrders.Services.Data.Interfaces
{
    public interface IRestaurantService
    {
        Task<List<string>> GetDistinctLocationsAsync();
        Task<List<RestaurantIndexViewModel>> GetRestaurantsByLocationAsync(string location);
        Task<RestaurantDetailsViewModel?> GetRestaurantDetailsAsync(int id);
    }
}
