using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyOrders.Web.ViewModels.Review;

namespace TastyOrders.Web.ViewModels.Restaurant
{
    public class RestaurantDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public List<ReviewViewModel> Reviews { get; set; } = new List<ReviewViewModel>();
    }
}
