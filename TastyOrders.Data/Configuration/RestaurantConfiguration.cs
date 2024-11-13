using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                }
            };

            return restaurants;
        }
    }
}
