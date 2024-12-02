using Microsoft.EntityFrameworkCore;
using TastyOrders.Data;
using TastyOrders.Data.Models;
using TastyOrders.Services.Data;

namespace TastyOrders.Services.Tests
{
    public class RestaurantServiceTests
    {
        private DbContextOptions<TastyOrdersDbContext> dbOptions;
        private TastyOrdersDbContext dbContext;
        private RestaurantService restaurantService;

        [SetUp]
        public void Setup()
        {
            dbOptions = new DbContextOptionsBuilder<TastyOrdersDbContext>()
                .UseInMemoryDatabase("TastyOrdersTestInMemory" + Guid.NewGuid())
                .Options;

            dbContext = new TastyOrdersDbContext(dbOptions);

            SeedDatabase(dbContext);

            restaurantService = new RestaurantService(dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        private void SeedDatabase(TastyOrdersDbContext context)
        {
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "1", UserName = "user1" },
                new ApplicationUser { Id = "2", UserName = "user2" }
            };

            var restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    Id = 1,
                    Name = "Restaurant Varna",
                    Location = "Varna",
                    ImageUrl = "varnaRest.jpg",
                    Reviews = new List<Review>
                    {
                        new Review { Id = 1, Rating = 5, Comment = "Great food!", UserId = "1", CreatedAt = DateTime.UtcNow },
                        new Review { Id = 2, Rating = 4, Comment = "Nice place.", UserId = "2", CreatedAt = DateTime.UtcNow }
                    }
                },
                new Restaurant
                {
                    Id = 2,
                    Name = "Restaurant Sofia",
                    Location = "Sofia",
                    ImageUrl = "sofiaRest.jpg",
                    Reviews = new List<Review>
                    {
                        new Review { Id = 3, Rating = 3, Comment = "Okay food.", UserId = "1", CreatedAt = DateTime.UtcNow }
                    }
                }
            };

            context.Users.AddRange(users);
            context.Restaurants.AddRange(restaurants);
            context.SaveChanges();
        }

        [Test]
        public async Task GetDistinctLocationsAsyncShouldReturnUniqueLocations()
        {
            var result = await restaurantService.GetDistinctLocationsAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Is.EquivalentTo(new[] { "Varna", "Sofia" }));
        }

        [Test]
        public async Task GetRestaurantsByLocationAsyncShouldReturnRestaurantsForValidLocation()
        {
            var result = await restaurantService.GetRestaurantsByLocationAsync("Varna", null);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Restaurant Varna"));
        }

        [Test]
        public async Task GetRestaurantsByLocationAsyncShouldReturnFilteredRestaurantsBySearchTerm()
        {
            var result = await restaurantService.GetRestaurantsByLocationAsync("Varna", "Restaurant Varna");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Restaurant Varna"));
        }

        [Test]
        public async Task GetRestaurantsByLocationAsyncShouldReturnEmptyListForInvalidSearchTerm()
        {
            var result = await restaurantService.GetRestaurantsByLocationAsync("Varna", "InvalidSearch");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetRestaurantDetailsAsyncShouldReturnDetailsForValidRestaurantId()
        {
            var result = await restaurantService.GetRestaurantDetailsAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name, Is.EqualTo("Restaurant Varna"));
            Assert.That(result.Reviews.Count, Is.EqualTo(2));
            Assert.That(result.Reviews.First().Comment, Is.EqualTo("Great food!"));
        }

        [Test]
        public async Task GetRestaurantDetailsAsyncShouldReturnNullForInvalidRestaurantId()
        {
            var result = await restaurantService.GetRestaurantDetailsAsync(99);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetRestaurantDetailsAsyncShouldHandleRestaurantWithoutReviews()
        {
            var newRestaurant = new Restaurant
            {
                Id = 3,
                Name = "Cafe",
                Location = "Sofia",
                Reviews = new List<Review>() 
            };
            dbContext.Restaurants.Add(newRestaurant);
            dbContext.SaveChanges();

            var result = await restaurantService.GetRestaurantDetailsAsync(3);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name, Is.EqualTo("Cafe"));
            Assert.That(result.Reviews.Count, Is.EqualTo(0));
        }
    }
}
