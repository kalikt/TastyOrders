using TastyOrders.Data.Models;

namespace TastyOrders.Services.Data.Interfaces
{
    public interface IReviewManagementService
    {
        Task<List<Review>> GetAllReviewsAsync();
        Task<bool> DeleteReviewAsync(int reviewId);
    }
}
