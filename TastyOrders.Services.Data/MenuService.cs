using Microsoft.EntityFrameworkCore;
using TastyOrders.Data;
using TastyOrders.Services.Data.Interfaces;
using TastyOrders.Web.ViewModels.Restaurant;

namespace TastyOrders.Services.Data
{
    public class MenuService : IMenuService
    {
        private readonly TastyOrdersDbContext context;

        public MenuService(TastyOrdersDbContext context)
        {
            this.context = context;
        }

        public async Task<RestaurantMenuViewModel?> GetRestaurantMenuAsync(int restaurantId)
        {
            return await context.Restaurants
                .Where(r => r.Id == restaurantId)
                .Select(r => new RestaurantMenuViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Location = r.Location,
                    MenuItems = r.MenuItems.Select(m => new MenuItemViewModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Description = m.Description,
                        ImageUrl = m.ImageUrl ?? string.Empty,
                        Price = m.Price
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }
    }
}
