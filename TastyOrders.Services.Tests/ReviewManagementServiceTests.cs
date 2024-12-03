using Microsoft.EntityFrameworkCore;
using TastyOrders.Data;
using TastyOrders.Data.Models;
using TastyOrders.Services.Data;

namespace TastyOrders.Services.Tests
{
    public class ReviewManagementServiceTests
    {
        private DbContextOptions<TastyOrdersDbContext> dbOptions;
        private TastyOrdersDbContext dbContext;
        private ReviewManagementService reviewService;

        [SetUp]
        public void Setup()
        {
            dbOptions = new DbContextOptionsBuilder<TastyOrdersDbContext>()
                .UseInMemoryDatabase("TastyOrdersInMemory" + Guid.NewGuid())
                .Options;

            dbContext = new TastyOrdersDbContext(dbOptions);

            SeedDatabase(dbContext);

            reviewService = new ReviewManagementService(dbContext);
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
                    RestaurantId = 2,
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
        public async Task GetAllReviewsAsyncShouldReturnAllReviews()
        {
            var result = await reviewService.GetAllReviewsAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));

            var firstReview = result.First();
            Assert.That(firstReview.RestaurantName, Is.EqualTo("Restaurant Varna"));
            Assert.That(firstReview.UserName, Is.EqualTo("user1"));
            Assert.That(firstReview.Rating, Is.EqualTo(5));
            Assert.That(firstReview.Comment, Is.EqualTo("Great food!"));
        }

        [Test]
        public async Task DeleteReviewAsyncShouldDeleteReviewForValidReviewId()
        {
            var reviewId = 1;

            var result = await reviewService.DeleteReviewAsync(reviewId);

            Assert.That(result, Is.True);
            var review = dbContext.Reviews.Find(reviewId);
            Assert.That(review, Is.Null);
        }

        [Test]
        public async Task DeleteReviewAsyncShouldReturnFalseForInvalidReviewId()
        {
            var invalidReviewId = 99;

            var result = await reviewService.DeleteReviewAsync(invalidReviewId);

            Assert.That(result, Is.False);
        }
    }
}