namespace TastyOrders.Web.ViewModels.Order
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        public string OrderDate { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public string RestaurantName { get; set; } = null!;
        public string RestaurantLocation { get; set; } = null!;
        public List<OrderItemViewModel> Items { get; set; } = new List<OrderItemViewModel>();
    }
}
