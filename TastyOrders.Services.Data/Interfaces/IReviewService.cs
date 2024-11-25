
using TastyOrders.Web.ViewModels.Review;

namespace TastyOrders.Services.Data.Interfaces
{
    public interface IReviewService
    {
        Task<ReviewCreateViewModel?> GetReviewCreateModelAsync(int restaurantId);
        Task<bool> AddReviewAsync(ReviewCreateViewModel model, string userId);
        Task<List<ReviewCreateViewModel>> GetReviewsByRestaurantAsync(int restaurantId);
        Task<List<MyReviewsViewModel>> GetUserReviewsAsync(string userId);
    }
}
