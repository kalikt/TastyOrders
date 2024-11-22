using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyOrders.Web.ViewModels.Restaurant
{
     public class RestaurantsWithLocationViewModel
    {
        public string SelectedLocation { get; set; } = null!;
        public IEnumerable<RestaurantIndexViewModel> Restaurants { get; set; } = new List<RestaurantIndexViewModel>();
    }
}
