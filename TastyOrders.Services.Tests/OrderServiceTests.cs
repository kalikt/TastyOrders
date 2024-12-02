using Microsoft.EntityFrameworkCore;
using TastyOrders.Data.Models;
using TastyOrders.Data;
using TastyOrders.Services.Data;

namespace TastyOrders.Services.Tests
{
    public class OrderServiceTests
    {
        private DbContextOptions<TastyOrdersDbContext> dbOptions;
        private TastyOrdersDbContext dbContext;
        private OrderService orderService;

        [SetUp]
        public void Setup()
        {
            dbOptions = new DbContextOptionsBuilder<TastyOrdersDbContext>()
                .UseInMemoryDatabase("TastyOrdersTestInMemory" + Guid.NewGuid())
                .Options;

            dbContext = new TastyOrdersDbContext(dbOptions);

            SeedDatabase(dbContext);

            orderService = new OrderService(dbContext);
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
                new Restaurant { Id = 1, Name = "Restaurant Varna", Location = "Varna" }
            };

            var menuItems = new List<MenuItem>
            {
                new MenuItem { Id = 1, Name = "Pizza", Description = "Delicious cheese pizza", Price = 10.99m, RestaurantId = 1 },
                new MenuItem { Id = 2, Name = "Pasta", Description = "Creamy Alfredo pasta", Price = 8.99m, RestaurantId = 1 }
            };

            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "user1", UserName = "testuser" }
            };

            var carts = new List<Cart>
            {
                new Cart
                {
                    UserId = "user1",
                    CartItems = new List<CartItem>
                    {
                        new CartItem { MenuItemId = 1, Quantity = 2 },
                        new CartItem { MenuItemId = 2, Quantity = 1 }
                    }
                }
            };

            var orders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    UserId = "user1",
                    OrderDate = DateTime.UtcNow,
                    TotalPrice = 29.97m,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem { MenuItemId = 1, Quantity = 2, Price = 10.99m },
                        new OrderItem { MenuItemId = 2, Quantity = 1, Price = 8.99m }
                    }
                }
            };

            context.Restaurants.AddRange(restaurants);
            context.MenuItems.AddRange(menuItems);
            context.Users.AddRange(users);
            context.Carts.AddRange(carts);
            context.Orders.AddRange(orders);
            context.SaveChanges();
        }

        [Test]
        public async Task GetUserOrdersAsyncShouldReturnOrdersForValidUser()
        {
            var result = await orderService.GetUserOrdersAsync("user1");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().TotalAmount, Is.EqualTo(29.97m));
        }

        [Test]
        public async Task GetUserOrdersAsyncShouldReturnEmptyListForInvalidUser()
        {
            var result = await orderService.GetUserOrdersAsync("invalidUser");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task PlaceOrderAsyncShouldCreateOrderWhenCartHasItems()
        {
            var result = await orderService.PlaceOrderAsync("user1");

            Assert.That(result, Is.True);

            var orders = dbContext.Orders.Include(o => o.OrderItems).ToList();
            Assert.That(orders.Count, Is.EqualTo(2));
            var newOrder = orders.Last();
            Assert.That(newOrder.TotalPrice, Is.EqualTo(30.97m));
            Assert.That(newOrder.OrderItems.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task PlaceOrderAsyncShouldReturnFalseWhenCartIsEmpty()
        {
            var emptyCart = new Cart { UserId = "emptyUser" };
            dbContext.Carts.Add(emptyCart);
            dbContext.SaveChanges();

            var result = await orderService.PlaceOrderAsync("emptyUser");

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetOrderDetailsAsyncShouldReturnDetailsForValidOrder()
        {
            var result = await orderService.GetOrderDetailsAsync(1, "user1");

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Id, Is.EqualTo(1));
            Assert.That(result.TotalAmount, Is.EqualTo(29.97m));
            Assert.That(result.Items.Count, Is.EqualTo(2));
            Assert.That(result.RestaurantName, Is.EqualTo("Restaurant Varna"));
        }

        [Test]
        public async Task GetOrderDetailsAsyncShouldReturnNullForInvalidOrderId()
        {
            var result = await orderService.GetOrderDetailsAsync(99, "user1");

            Assert.That(result, Is.Null);
        }
    }
}
