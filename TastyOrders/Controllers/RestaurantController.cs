﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TastyOrders.Data;
using TastyOrders.Web.ViewModels.Restaurant;

namespace TastyOrders.Web.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly TastyOrdersDbContext context;

        public RestaurantController(TastyOrdersDbContext context)
        {
            this.context = context;
        }

        // GET: Restaurant
        public async Task<IActionResult> Index()
        {
            var restaurants = await context.Restaurants
                .Select(r => new RestaurantIndexViewModel
            {
                Id = r.Id,
                Name = r.Name,
                Location = r.Location,
                ImageUrl = r.ImageUrl ?? string.Empty
            }).ToListAsync();

            return View(restaurants);
        }

        // GET: Restaurant/Menu/5
        public async Task<IActionResult> Menu(int id)
        {
            var restaurant = await context.Restaurants
            .Where(r => r.Id == id)
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

            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

    }
}