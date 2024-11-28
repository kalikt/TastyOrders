using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyOrders.Web.ViewModels.Restaurant
{
    public class ManageRestaurantsViewModel
    {
        public IEnumerable<RestaurantViewModel> Restaurants { get; set; } =
            new List<RestaurantViewModel>();
    }

    public class RestaurantViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string? ImageUrl { get; set; } = null!;
    }
}
