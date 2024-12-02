using Microsoft.EntityFrameworkCore;
using TastyOrders.Data;
using TastyOrders.Data.Models;
using TastyOrders.Services.Data;

namespace TastyOrders.Services.Tests
{
    public class MenuServiceTests
    {
        private DbContextOptions<TastyOrdersDbContext> dbOptions;
        private TastyOrdersDbContext dbContext;
        private MenuService menuService;

        [SetUp]
        public void Setup()
        {
            dbOptions = new DbContextOptionsBuilder<TastyOrdersDbContext>()
                .UseInMemoryDatabase("TastyOrdersInMemory" + System.Guid.NewGuid())
                .Options;

            dbContext = new TastyOrdersDbContext(dbOptions);

            SeedDatabase(dbContext);

            menuService = new MenuService(dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        private void SeedDatabase(TastyOrdersDbContext context)
        {
            var restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    Id = 1,
                    Name = "Restaurant Varna",
                    Location = "Varna",
                    MenuItems = new List<MenuItem>
                    {
                        new MenuItem { Id = 1, Name = "Pizza", Description = "Delicious cheese pizza", Price = 10.99m, ImageUrl = "pizza.jpg" },
                        new MenuItem { Id = 2, Name = "Pasta", Description = "Creamy Alfredo pasta", Price = 8.99m, ImageUrl = "pasta.jpg" }
                    }
                },
                new Restaurant
                {
                    Id = 2,
                    Name = "Restaurant Sofia",
                    Location = "Sofia",
                    MenuItems = new List<MenuItem>
                    {
                        new MenuItem { Id = 3, Name = "Salad", Description = "Fresh garden salad", Price = 6.99m, ImageUrl = "salad.jpg" }
                    }
                }
            };

            context.Restaurants.AddRange(restaurants);
            context.SaveChanges();
        }

        [Test]
        public async Task GetRestaurantMenuAsyncShouldReturnMenuForValidRestaurantId()
        {
            var result = await menuService.GetRestaurantMenuAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Restaurant Varna"));
            Assert.That(result.Location, Is.EqualTo("Varna"));
            Assert.That(result.MenuItems.Count, Is.EqualTo(2));

            var firstMenuItem = result.MenuItems.First();
            Assert.That(firstMenuItem.Id, Is.EqualTo(1));
            Assert.That(firstMenuItem.Name, Is.EqualTo("Pizza"));
            Assert.That(firstMenuItem.Description, Is.EqualTo("Delicious cheese pizza"));
            Assert.That(firstMenuItem.Price, Is.EqualTo(10.99m));
            Assert.That(firstMenuItem.ImageUrl, Is.EqualTo("pizza.jpg"));
        }

        [Test]
        public async Task GetRestaurantMenuAsyncShouldReturnNullForInvalidRestaurantId()
        {
            var result = await menuService.GetRestaurantMenuAsync(99);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetRestaurantMenuAsyncShouldHandleRestaurantWithoutMenuItems()
        {
            var newRestaurant = new Restaurant
            {
                Id = 3,
                Name = "Cafe",
                Location = "Sofia",
                MenuItems = new List<MenuItem>() 
            };
            dbContext.Restaurants.Add(newRestaurant);
            dbContext.SaveChanges();

            var result = await menuService.GetRestaurantMenuAsync(3);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Id, Is.EqualTo(3));
            Assert.That(result.Name, Is.EqualTo("Cafe"));
            Assert.That(result.Location, Is.EqualTo("Sofia"));
            Assert.That(result.MenuItems.Count, Is.EqualTo(0));
        }
    }
}
