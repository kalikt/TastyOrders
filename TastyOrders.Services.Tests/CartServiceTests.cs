using Microsoft.EntityFrameworkCore;
using TastyOrders.Data.Models;
using TastyOrders.Data;
using TastyOrders.Services.Data;

namespace TastyOrders.Services.Tests
{
    public class CartServiceTests
    {
        private DbContextOptions<TastyOrdersDbContext> dbOptions;
        private TastyOrdersDbContext dbContext;
        private CartService cartService;

        [SetUp]
        public void Setup()
        {
            dbOptions = new DbContextOptionsBuilder<TastyOrdersDbContext>()
                .UseInMemoryDatabase("TastyOrdersInMemory" + Guid.NewGuid())
                .Options;

            dbContext = new TastyOrdersDbContext(dbOptions);

            SeedDatabase(dbContext);

            cartService = new CartService(dbContext);
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
                new Restaurant { Id = 1, Name = "Restaurant Varna", Location = "Varna" },
                new Restaurant { Id = 2, Name = "Restaurant Sofia", Location = "Sofia" }
            };

            var menuItems = new List<MenuItem>
            {
                new MenuItem { Id = 1, Name = "Pizza", Description = "Delicious cheese pizza", Price = 10.99m, RestaurantId = 1 },
                new MenuItem { Id = 2, Name = "Pasta", Description = "Creamy Alfredo pasta", Price = 8.99m, RestaurantId = 1 },
                new MenuItem { Id = 3, Name = "Salad", Description = "Fresh garden salad", Price = 6.99m, RestaurantId = 2 }
            };


            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "user1", UserName = "user1" },
                new ApplicationUser { Id = "user2", UserName = "user2" }
            };

            var carts = new List<Cart>
            {
                new Cart
                {
                    UserId = "user1",
                    CartItems = new List<CartItem>
                    {
                        new CartItem { MenuItemId = 1, Quantity = 2 }
                    }
                }
            };

            context.Restaurants.AddRange(restaurants);
            context.MenuItems.AddRange(menuItems);
            context.Users.AddRange(users);
            context.Carts.AddRange(carts);
            context.SaveChanges();
        }

        [Test]
        public async Task AddToCartAsyncShouldAddItemWhenCartDoesNotExist()
        {
            var result = await cartService.AddToCartAsync("user2", 2);

            Assert.That(result, Is.True);
            var cart = dbContext.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.UserId == "user2");
            Assert.That(cart, Is.Not.Null);
            Assert.That(cart.CartItems.Count, Is.EqualTo(1));
            Assert.That(cart.CartItems.First().MenuItemId, Is.EqualTo(2));
        }

        [Test]
        public async Task AddToCartAsyncShouldIncrementQuantityWhenItemAlreadyInCart()
        {
            var result = await cartService.AddToCartAsync("user1", 1);

            Assert.That(result, Is.True);
            var cart = dbContext.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.UserId == "user1");
            Assert.That(cart.CartItems.First().Quantity, Is.EqualTo(3));
        }

        [Test]
        public async Task AddToCartAsyncShouldReturnFalseWhenAddingFromDifferentLocation()
        {
            var result = await cartService.AddToCartAsync("user1", 3);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetCartAsyncShouldReturnCartForValidUser()
        {
            var result = await cartService.GetCartAsync("user1");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items.Count, Is.EqualTo(1));
            Assert.That(result.Items.First().Name, Is.EqualTo("Pizza"));
            Assert.That(result.SelectedLocation, Is.EqualTo("Varna"));
        }

        [Test]
        public async Task GetCartAsyncShouldReturnEmptyCartForInvalidUser()
        {
            var result = await cartService.GetCartAsync("invalidUser");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items.Count, Is.EqualTo(0));
            Assert.That(result.SelectedLocation, Is.Null);
        }

        [Test]
        public async Task UpdateQuantityAsyncShouldUpdateQuantityForValidCartItem()
        {
            var cartItem = dbContext.CartItems.First();
            var result = await cartService.UpdateQuantityAsync(cartItem.Id, 5);

            Assert.That(result, Is.True);
            Assert.That(cartItem.Quantity, Is.EqualTo(5));
        }

        [Test]
        public async Task UpdateQuantityAsyncShouldReturnFalseForInvalidCartItem()
        {
            var result = await cartService.UpdateQuantityAsync(99, 5);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task UpdateQuantityAsyncShouldReturnFalseWhenQuantityIsLessThanOne()
        {
            var cartItem = dbContext.CartItems.First();
            var result = await cartService.UpdateQuantityAsync(cartItem.Id, 0);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task RemoveItemAsyncShouldRemoveItemForValidCartItem()
        {
            var cartItem = dbContext.CartItems.First();

            var result = await cartService.RemoveItemAsync(cartItem.Id);

            Assert.That(result, Is.True);
            Assert.That(dbContext.CartItems.Any(ci => ci.Id == cartItem.Id), Is.False);
        }

        [Test]
        public async Task RemoveItemAsyncShouldReturnFalseForInvalidCartItem()
        {
            var result = await cartService.RemoveItemAsync(99);

            Assert.That(result, Is.False);
        }
    }
}
