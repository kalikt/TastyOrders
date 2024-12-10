namespace TastyOrders.Web.ViewModels.MenuItem
{
    public class ManageMenuItemsViewModel
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; } = null!;
        public IEnumerable<MenuItemViewModel> MenuItems { get; set; } 
            = new List<MenuItemViewModel>();
    }

    public class MenuItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
    }
}
