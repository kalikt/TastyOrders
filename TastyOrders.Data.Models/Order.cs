namespace TastyOrders.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } 
            = new List<OrderItem>();
    }
}
