using Microsoft.EntityFrameworkCore;
using TastyOrders.Data;
using TastyOrders.Data.Models;
using TastyOrders.Services.Data;
using TastyOrders.Web.ViewModels.MenuItem;

namespace TastyOrders.Services.Tests
{
    public class MenuItemManagementServiceTests
    {
        private DbContextOptions<TastyOrdersDbContext> dbOptions;
        private TastyOrdersDbContext dbContext;
        private MenuItemManagementService menuItemService;

        [SetUp]
        public void Setup()
        {
            dbOptions = new DbContextOptionsBuilder<TastyOrdersDbContext>()
                .UseInMemoryDatabase("TastyOrdersInMemory" + Guid.NewGuid())
                .Options;

            dbContext = new TastyOrdersDbContext(dbOptions);

            SeedDatabase(dbContext);

            menuItemService = new MenuItemManagementService(dbContext);
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
                new Restaurant { Id = 1, Name = "Restaurant Varna", Location = "Varna", ImageUrl = "image1.jpg" },
                new Restaurant { Id = 2, Name = "Restaurant Sofia", Location = "Sofia", ImageUrl = "image2.jpg" }
            };

            var menuItems = new List<MenuItem>
            {
                new MenuItem { Id = 1, Name = "Pizza", Price = 10.99m, Description = "Cheesy pizza", RestaurantId = 1 },
                new MenuItem { Id = 2, Name = "Pasta", Price = 12.99m, Description = "Delicious pasta", RestaurantId = 1 },
                new MenuItem { Id = 3, Name = "Burger", Price = 8.99m, Description = "Tasty burger", RestaurantId = 2 }
            };

            context.Restaurants.AddRange(restaurants);
            context.MenuItems.AddRange(menuItems);
            context.SaveChanges();
        }

        [Test]
        public async Task GetRestaurantWithMenuItemsAsyncShouldReturnRestaurantWithMenuItems()
        {
            var result = await menuItemService.GetRestaurantWithMenuItemsAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.MenuItems.Count, Is.EqualTo(2));
            Assert.That(result.MenuItems.Any(mi => mi.Name == "Pizza"), Is.True);
        }

        [Test]
        public async Task GetRestaurantWithMenuItemsAsyncShouldReturnNullForNonExistentRestaurant()
        {
            var result = await menuItemService.GetRestaurantWithMenuItemsAsync(999);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task AddMenuItemAsyncShouldAddMenuItemWithValidData()
        {
            var result = await menuItemService.AddMenuItemAsync(1, "Salad", 5.99m, "Fresh salad", "image3.jpg");

            Assert.That(result, Is.True);
            var menuItems = dbContext.MenuItems.Where(mi => mi.RestaurantId == 1).ToList();
            Assert.That(menuItems.Count, Is.EqualTo(3));
            Assert.That(menuItems.Any(mi => mi.Name == "Salad"), Is.True);
        }

        [Test]
        public async Task AddMenuItemAsyncShouldReturnFalseForInvalidData()
        {
            var result = await menuItemService.AddMenuItemAsync(1, "", 0, "", null);

            Assert.That(result, Is.False);
            Assert.That(dbContext.MenuItems.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task RemoveMenuItemAsyncShouldRemoveMenuItemWhenExists()
        {
            var result = await menuItemService.RemoveMenuItemAsync(1);

            Assert.That(result, Is.EqualTo(1));
            Assert.That(dbContext.MenuItems.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task RemoveMenuItemAsyncShouldReturnNullWhenMenuItemDoesNotExist()
        {
            var result = await menuItemService.RemoveMenuItemAsync(999);

            Assert.That(result, Is.Null);
            Assert.That(dbContext.MenuItems.Count(), Is.EqualTo(3)); 
        }

        [Test]
        public async Task GetMenuItemByIdAsyncShouldReturnMenuItemWhenExists()
        {
            var result = await menuItemService.GetMenuItemByIdAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name, Is.EqualTo("Pizza"));
            Assert.That(result.Price, Is.EqualTo(10.99m));
        }

        [Test]
        public async Task GetMenuItemByIdAsyncShouldReturnNullWhenMenuItemDoesNotExist()
        {
            var result = await menuItemService.GetMenuItemByIdAsync(999);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task UpdateMenuItemAsyncShouldUpdateMenuItemWithValidData()
        {
            var updatedMenuItem = new EditMenuItemViewModel
            {
                Id = 1,
                Name = "Updated Pizza",
                Price = 11.99m,
                Description = "Updated description",
                ImageUrl = "updatedImage.jpg",
                RestaurantId = 1
            };

            var result = await menuItemService.UpdateMenuItemAsync(updatedMenuItem);

            Assert.That(result, Is.True);

            var menuItem = await dbContext.MenuItems.FindAsync(1);
            Assert.That(menuItem, Is.Not.Null);
            Assert.That(menuItem!.Name, Is.EqualTo("Updated Pizza"));
            Assert.That(menuItem.Price, Is.EqualTo(11.99m));
        }

        [Test]
        public async Task UpdateMenuItemAsyncShouldReturnFalseForInvalidData()
        {
            var updatedMenuItem = new EditMenuItemViewModel
            {
                Id = 1,
                Name = "",
                Price = 0,
                Description = "",
                ImageUrl = null ?? string.Empty,
                RestaurantId = 1
            };

            var result = await menuItemService.UpdateMenuItemAsync(updatedMenuItem);

            Assert.That(result, Is.False);

            var menuItem = await dbContext.MenuItems.FindAsync(1);
            Assert.That(menuItem!.Name, Is.EqualTo("Pizza")); 
        }

        [Test]
        public async Task UpdateMenuItemAsyncShouldReturnFalseWhenMenuItemDoesNotExist()
        {
            var updatedMenuItem = new EditMenuItemViewModel
            {
                Id = 999,
                Name = "Nonexistent",
                Price = 11.99m,
                Description = "Updated description",
                ImageUrl = "updatedImage.jpg",
                RestaurantId = 1
            };

            var result = await menuItemService.UpdateMenuItemAsync(updatedMenuItem);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetManageMenuItemsViewModelAsyncShouldReturnViewModelWhenRestaurantExists()
        {
            var result = await menuItemService.GetManageMenuItemsViewModelAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.MenuItems.Count, Is.EqualTo(2));
            Assert.That(result.RestaurantName, Is.EqualTo("Restaurant Varna"));
        }

        [Test]
        public async Task GetManageMenuItemsViewModelAsyncShouldReturnNullWhenRestaurantDoesNotExist()
        {
            var result = await menuItemService.GetManageMenuItemsViewModelAsync(999);

            Assert.That(result, Is.Null);
        }
    }
}
