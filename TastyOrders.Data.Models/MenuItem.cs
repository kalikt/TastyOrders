namespace TastyOrders.Data.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; } = null!;
    }
}
