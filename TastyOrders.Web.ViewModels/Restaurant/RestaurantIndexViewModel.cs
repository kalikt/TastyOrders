namespace TastyOrders.Web.ViewModels.Restaurant
{
    public class RestaurantIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}
