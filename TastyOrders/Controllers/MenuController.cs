using Microsoft.AspNetCore.Mvc;
using TastyOrders.Services.Data.Interfaces;

namespace TastyOrders.Web.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService menuService;

        public MenuController(IMenuService menuService)
        {
            this.menuService = menuService;
        }

        [HttpGet]
        public async Task<IActionResult> Menu(int id)
        {
            var restaurantMenu = await menuService.GetRestaurantMenuAsync(id);

            if (restaurantMenu == null)
            {
                return NotFound();
            }

            return View(restaurantMenu);
        }
    }
}
