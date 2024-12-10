namespace TastyOrders.Web.ViewModels.Review
{
    public class MyReviewsViewModel
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; } = null!;
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
        public string CreatedAt { get; set; } = null!;
    }
}
