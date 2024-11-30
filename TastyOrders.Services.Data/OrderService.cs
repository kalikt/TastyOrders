using TastyOrders.Data.Models;
using TastyOrders.Data;
using TastyOrders.Services.Data.Interfaces;
using TastyOrders.Web.ViewModels.Order;
using Microsoft.EntityFrameworkCore;

namespace TastyOrders.Services.Data
{
    using static Common.EntityValidationConstants.Order;
    public class OrderService : IOrderService
    {
        private readonly TastyOrdersDbContext context;

        public OrderService(TastyOrdersDbContext context)
        {
            this.context = context;
        }

        public async Task<List<OrderSummaryViewModel>> GetUserOrdersAsync(string userId)
        {
            return await context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .Select(o => new OrderSummaryViewModel
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate.ToString(OrderDateFormat),
                    TotalAmount = o.TotalPrice,
                    Items = o.OrderItems.Select(oi => oi.MenuItem.Name).ToList()
                })
                .ToListAsync();
        }

        public async Task<bool> PlaceOrderAsync(string userId)
        {
            var cart = await context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.MenuItem)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartItems.Any())
            {
                return false;
            }

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                TotalPrice = cart.CartItems.Sum(ci => ci.Quantity * ci.MenuItem.Price),
                OrderItems = cart.CartItems.Select(ci => new OrderItem
                {
                    MenuItemId = ci.MenuItemId,
                    Quantity = ci.Quantity,
                    Price = ci.MenuItem.Price
                }).ToList()
            };

            context.Orders.Add(order);
            context.CartItems.RemoveRange(cart.CartItems); 
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<OrderDetailsViewModel?> GetOrderDetailsAsync(int orderId, string userId)
        {
            var order = await context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .ThenInclude(mi => mi.Restaurant)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);

            if (order == null)
            {
                return null;
            }

            return new OrderDetailsViewModel
            {
                Id = order.Id,
                OrderDate = order.OrderDate.ToString(OrderDateFormat),
                TotalAmount = order.TotalPrice,
                RestaurantName = order.OrderItems.FirstOrDefault()?.MenuItem.Restaurant.Name ?? string.Empty,
                RestaurantLocation = order.OrderItems.FirstOrDefault()?.MenuItem.Restaurant.Location ?? string.Empty,
                Items = order.OrderItems.Select(oi => new OrderItemViewModel
                {
                    Name = oi.MenuItem.Name,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                })
                .ToList()
            };
        }
    }
}
