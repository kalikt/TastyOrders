using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyOrders.Web.ViewModels.Restaurant;

namespace TastyOrders.Services.Data.Interfaces
{
    public interface IMenuService
    {
        Task<RestaurantMenuViewModel?> GetRestaurantMenuAsync(int restaurantId);
    }
}
