namespace TastyOrders.Web.ViewModels.Review
{
    public class ReviewViewModel
    {
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string CreatedAt { get; set; } = null!;
    }
}
