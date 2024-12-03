using Microsoft.EntityFrameworkCore;
using TastyOrders.Data;
using TastyOrders.Data.Models;
using TastyOrders.Services.Data;
using TastyOrders.Web.ViewModels.Review;

namespace TastyOrders.Services.Tests
{
    public class ReviewServiceTests
    {
        private DbContextOptions<TastyOrdersDbContext> dbOptions;
        private TastyOrdersDbContext dbContext;
        private ReviewService reviewService;

        [SetUp]
        public void Setup()
        {
            dbOptions = new DbContextOptionsBuilder<TastyOrdersDbContext>()
                .UseInMemoryDatabase("TastyOrdersInMemory" + Guid.NewGuid())
                .Options;

            dbContext = new TastyOrdersDbContext(dbOptions);

            SeedDatabase(dbContext);

            reviewService = new ReviewService(dbContext);
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
                new ApplicationUser { Id = "user1", UserName = "user1" },
                new ApplicationUser { Id = "user2", UserName = "user2" }
            };

            var restaurants = new List<Restaurant>
            {
                new Restaurant { Id = 1, Name = "Restaurant Varna", Location = "Varna" },
                new Restaurant { Id = 2, Name = "Restaurant Sofia", Location = "Sofia" }
            };

            var reviews = new List<Review>
            {
                new Review
                {
                    Id = 1,
                    RestaurantId = 1,
                    UserId = "user1",
                    Rating = 5,
                    Comment = "Great food!",
                    CreatedAt = DateTime.UtcNow
                },
                new Review
                {
                    Id = 2,
                    RestaurantId = 1,
                    UserId = "user2",
                    Rating = 4,
                    Comment = "Good service.",
                    CreatedAt = DateTime.UtcNow
                }
            };

            context.Users.AddRange(users);
            context.Restaurants.AddRange(restaurants);
            context.Reviews.AddRange(reviews);
            context.SaveChanges();
        }

        [Test]
        public async Task GetReviewCreateModelAsyncShouldReturnModelForValidRestaurantId()
        {
            var result = await reviewService.GetReviewCreateModelAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.RestaurantId, Is.EqualTo(1));
            Assert.That(result.RestaurantName, Is.EqualTo("Restaurant Varna"));
        }

        [Test]
        public async Task GetReviewCreateModelAsyncShouldReturnNullForInvalidRestaurantId()
        {
            var result = await reviewService.GetReviewCreateModelAsync(99);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task AddReviewAsyncShouldAddReviewWhenModelIsValid()
        {
            var model = new ReviewCreateViewModel
            {
                RestaurantId = 1,
                Rating = 4,
                Comment = "Nice place."
            };

            var result = await reviewService.AddReviewAsync(model, "user1");

            Assert.That(result, Is.True);
            var reviews = dbContext.Reviews.Where(r => r.RestaurantId == 1).ToList();
            Assert.That(reviews.Count, Is.EqualTo(3));
            Assert.That(reviews.Last().Comment, Is.EqualTo("Nice place."));
        }

        [Test]
        public async Task GetReviewsByRestaurantAsyncShouldReturnReviewsForValidRestaurantId()
        {
            var result = await reviewService.GetReviewsByRestaurantAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.First().RestaurantName, Is.EqualTo("Restaurant Varna"));
        }

        [Test]
        public async Task GetReviewsByRestaurantAsyncShouldReturnEmptyListForInvalidRestaurantId()
        {
            var result = await reviewService.GetReviewsByRestaurantAsync(99);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetUserReviewsAsyncShouldReturnReviewsForValidUserId()
        {
            var result = await reviewService.GetUserReviewsAsync("user1");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Comment, Is.EqualTo("Great food!"));
            Assert.That(result.First().CreatedAt, Is.Not.Empty);
        }

        [Test]
        public async Task GetUserReviewsAsyncShouldReturnEmptyListForInvalidUserId()
        {
            var result = await reviewService.GetUserReviewsAsync("invalidUser");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }
    }
}
