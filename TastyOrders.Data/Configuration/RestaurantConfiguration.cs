using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TastyOrders.Data.Models;

namespace TastyOrders.Data.Configuration
{
    using static TastyOrders.Common.EntityValidationConstants.Restaurant;
    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder
                .HasKey(r => r.Id);

            builder
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            builder
                .Property(r => r.Location)
                .IsRequired()
                .HasMaxLength(LocationMaxLength);

            builder
                .Property(r => r.ImageUrl)
                .HasMaxLength(ImageUrlMaxLength);

            builder.HasMany(r => r.MenuItems)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(this.SeedRestaurants());
        }

        private List<Restaurant> SeedRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Id = 1,
                    Name = "Smash Burgers",
                    Location = "Sofia",
                    ImageUrl = "/images/smashBurger.png"
                },
                new Restaurant()
                {
                    Id = 2,
                    Name = "Taste of Italy",
                    Location = "Varna",
                    ImageUrl = "/images/italyRestaurant.jpg"
                },
                new Restaurant()
                {
                    Id = 3,
                    Name = "Sweet Place",
                    Location = "Veliko Tarnovo",
                    ImageUrl = "/images/sweetPlace.jpg"
                },
                new Restaurant()
                {
                    Id = 4,
                    Name = "Soup & Salad",
                    Location = "Sofia",
                    ImageUrl = "/images/soupAndSaladRestaurant.jpg"
                }, 
                new Restaurant()
                {
                    Id = 5,
                    Name = "Taco Spot",
                    Location = "Sofia",
                    ImageUrl = "/images/tacoSpot.jpg"
                },
                new Restaurant()
                {
                    Id = 6,
                    Name = "Sunny Diner",
                    Location = "Varna",
                    ImageUrl = "/images/sunnyDiner.jpg"
                },
                new Restaurant()
                {
                    Id = 7,
                    Name = "Endorfino",
                    Location = "Varna",
                    ImageUrl = "/images/endorfino.jpg"
                },
                new Restaurant()
                {
                    Id = 8,
                    Name = "Pasta Place",
                    Location = "Veliko Tarnovo",
                    ImageUrl = "/images/pastaPlace.jpg"
                },
                new Restaurant()
                {
                    Id = 9,
                    Name = "Pizza Corner",
                    Location = "Veliko Tarnovo",
                    ImageUrl = "/images/pizzaPlace.jpg"
                }
            };

            return restaurants;
        }
    }
}
