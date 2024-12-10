namespace TastyOrders.Data.Models
{
    public class Review 
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; } = null!;

    }
}
