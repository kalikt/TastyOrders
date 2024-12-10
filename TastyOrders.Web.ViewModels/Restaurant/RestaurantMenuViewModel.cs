namespace TastyOrders.Web.ViewModels.Restaurant
{

    public class MenuItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public decimal Price { get; set; }
    }

    public class RestaurantMenuViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public IEnumerable<MenuItemViewModel> MenuItems { get; set; } = 
            new List<MenuItemViewModel>();
    }
}
