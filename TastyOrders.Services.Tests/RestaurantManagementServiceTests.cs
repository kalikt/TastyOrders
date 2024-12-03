using Microsoft.EntityFrameworkCore;
using TastyOrders.Data;
using TastyOrders.Data.Models;
using TastyOrders.Services.Data;
using TastyOrders.Web.ViewModels.Restaurant;

namespace TastyOrders.Services.Tests
{
    public class RestaurantManagementServiceTests
    {
        private DbContextOptions<TastyOrdersDbContext> dbOptions;
        private TastyOrdersDbContext dbContext;
        private RestaurantManagementService restaurantService;

        [SetUp]
        public void Setup()
        {
            dbOptions = new DbContextOptionsBuilder<TastyOrdersDbContext>()
                .UseInMemoryDatabase("TastyOrdersInMemory" + Guid.NewGuid())
                .Options;

            dbContext = new TastyOrdersDbContext(dbOptions);

            SeedDatabase(dbContext);

            restaurantService = new RestaurantManagementService(dbContext);
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

            context.Restaurants.AddRange(restaurants);
            context.SaveChanges();
        }

        [Test]
        public async Task GetAllRestaurantsAsyncShouldReturnAllRestaurants()
        {
            var result = await restaurantService.GetAllRestaurantsAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(r => r.Name == "Restaurant Varna"), Is.True);
            Assert.That(result.Any(r => r.Name == "Restaurant Sofia"), Is.True);
        }

        [Test]
        public async Task AddRestaurantAsyncShouldAddRestaurantWhenValidDataProvided()
        {
            var result = await restaurantService.AddRestaurantAsync("New Restaurant", "Plovdiv", "image3.jpg");

            Assert.That(result, Is.True);
            Assert.That(dbContext.Restaurants.Count(), Is.EqualTo(3));

            var addedRestaurant = dbContext.Restaurants.FirstOrDefault(r => r.Name == "New Restaurant");
            Assert.That(addedRestaurant, Is.Not.Null);
            Assert.That(addedRestaurant.Location, Is.EqualTo("Plovdiv"));
            Assert.That(addedRestaurant.ImageUrl, Is.EqualTo("image3.jpg"));
        }

        [Test]
        public async Task AddRestaurantAsyncShouldReturnFalseWhenInvalidDataProvided()
        {
            var result = await restaurantService.AddRestaurantAsync("", "Plovdiv", "image3.jpg");

            Assert.That(result, Is.False);
            Assert.That(dbContext.Restaurants.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task RemoveRestaurantAsyncShouldRemoveRestaurantWhenIdExists()
        {
            var result = await restaurantService.RemoveRestaurantAsync(1);

            Assert.That(result, Is.True);
            Assert.That(dbContext.Restaurants.Count(), Is.EqualTo(1));
            Assert.That(dbContext.Restaurants.Any(r => r.Id == 1), Is.False);
        }

        [Test]
        public async Task RemoveRestaurantAsyncShouldReturnFalseWhenIdDoesNotExist()
        {
            var result = await restaurantService.RemoveRestaurantAsync(99);

            Assert.That(result, Is.False);
            Assert.That(dbContext.Restaurants.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetRestaurantByIdAsyncShouldReturnRestaurantWhenIdExists()
        {
            var result = await restaurantService.GetRestaurantByIdAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name, Is.EqualTo("Restaurant Varna"));
            Assert.That(result.Location, Is.EqualTo("Varna"));
            Assert.That(result.ImageUrl, Is.EqualTo("image1.jpg"));
        }

        [Test]
        public async Task GetRestaurantByIdAsyncShouldReturnNullWhenIdDoesNotExist()
        {
            var result = await restaurantService.GetRestaurantByIdAsync(99);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task EditRestaurantAsyncShouldUpdateRestaurantWhenValidDataProvided()
        {
            var editModel = new EditRestaurantViewModel
            {
                Id = 1,
                Name = "Updated Name",
                Location = "Updated Location",
                ImageUrl = "updatedImage.jpg"
            };

            var result = await restaurantService.EditRestaurantAsync(editModel);

            Assert.That(result, Is.True);

            var updatedRestaurant = await dbContext.Restaurants.FindAsync(1);
            Assert.That(updatedRestaurant, Is.Not.Null);
            Assert.That(updatedRestaurant!.Name, Is.EqualTo("Updated Name"));
            Assert.That(updatedRestaurant.Location, Is.EqualTo("Updated Location"));
            Assert.That(updatedRestaurant.ImageUrl, Is.EqualTo("updatedImage.jpg"));
        }

        [Test]
        public async Task EditRestaurantAsyncShouldReturnFalseWhenInvalidDataProvided()
        {
            var editModel = new EditRestaurantViewModel
            {
                Id = 1,
                Name = "",
                Location = "",
                ImageUrl = "updatedImage.jpg"
            };

            var result = await restaurantService.EditRestaurantAsync(editModel);

            Assert.That(result, Is.False);

            var restaurant = await dbContext.Restaurants.FindAsync(1);
            Assert.That(restaurant!.Name, Is.EqualTo("Restaurant Varna"));
        }

        [Test]
        public async Task EditRestaurantAsyncShouldReturnFalseWhenRestaurantDoesNotExist()
        {
            var editModel = new EditRestaurantViewModel
            {
                Id = 99,
                Name = "Nonexistent",
                Location = "Nonexistent Location",
                ImageUrl = "nonexistentImage.jpg"
            };

            var result = await restaurantService.EditRestaurantAsync(editModel);

            Assert.That(result, Is.False);
        }
    }
}
