namespace TastyOrders.Web.ViewModels.Order
{
    public class OrderItemViewModel
    {
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
