namespace TastyOrders.Web.ViewModels.Cart
{
    public class CartItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; } 
        public int Quantity { get; set; } 
        public decimal Total => Price * Quantity;
    }
}
