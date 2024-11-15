using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;
using TastyOrders.Data.Configuration;
using TastyOrders.Data.Models;

namespace TastyOrders.Data;

public class TastyOrdersDbContext : IdentityDbContext<ApplicationUser>
{
    public TastyOrdersDbContext()
    {

    }
    public TastyOrdersDbContext(DbContextOptions<TastyOrdersDbContext> options)
        : base(options)
    {
    }

    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.ApplyConfiguration(new ApplicationUserConfiguration());
        builder.ApplyConfiguration(new RestaurantConfiguration());
        builder.ApplyConfiguration(new MenuItemConfiguration());
        builder.ApplyConfiguration(new OrderConfiguration());
        builder.ApplyConfiguration(new OrderItemConfiguration());
        builder.ApplyConfiguration(new CartConfiguration());
        builder.ApplyConfiguration(new CartItemConfiguration());
        builder.ApplyConfiguration(new ReviewConfiguration());

        base.OnModelCreating(builder);
    }
}
