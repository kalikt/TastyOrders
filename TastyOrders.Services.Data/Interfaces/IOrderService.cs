using TastyOrders.Web.ViewModels.Order;

namespace TastyOrders.Services.Data.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderSummaryViewModel>> GetUserOrdersAsync(string userId);
        Task<int?> PlaceOrderAsync(string userId);
        Task<OrderDetailsViewModel?> GetOrderDetailsAsync(int orderId, string userId);
    }
}
