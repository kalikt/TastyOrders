using TastyOrders.Data.Models;
using TastyOrders.Web.ViewModels.Review;

namespace TastyOrders.Services.Data.Interfaces
{
    public interface IReviewManagementService
    {
        Task<List<ReviewIndexViewModel>> GetAllReviewsAsync();
        Task<bool> DeleteReviewAsync(int reviewId);
    }
}
