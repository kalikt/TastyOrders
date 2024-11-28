using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyOrders.Web.ViewModels.Review
{
    public class ManageReviewsViewModel
    {
        public IEnumerable<ReviewIndexViewModel> Reviews { get; set; }
        = new List<ReviewIndexViewModel>();
    }

    public class ReviewIndexViewModel
    {
        public int Id { get; set; }
        public string RestaurantName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
    }
}
