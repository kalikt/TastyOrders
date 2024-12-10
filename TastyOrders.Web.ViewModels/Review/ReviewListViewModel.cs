namespace TastyOrders.Web.ViewModels.Review
{
    public class ReviewListViewModel
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;

        public string Location {  get; set; } = string.Empty;
    }
}
