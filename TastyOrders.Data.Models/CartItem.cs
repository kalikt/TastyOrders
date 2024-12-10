namespace TastyOrders.Data.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; } = null!;
        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; } = null!;
    }
}
