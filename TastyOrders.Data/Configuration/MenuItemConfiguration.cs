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
    using static TastyOrders.Common.EntityValidationConstants.MenuItem;
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            builder
                .Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(DescriptionMaxLength);

            builder
                .Property(m => m.ImageUrl)
                .HasMaxLength(ImageUrlMaxLength);

            builder
                .Property(t => t.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder
                .HasOne(mi => mi.Restaurant)
                .WithMany(r => r.MenuItems)
                .HasForeignKey(mi => mi.RestaurantId);

            builder.HasData(this.SeedMenuItems());
        }

        private List<MenuItem> SeedMenuItems()
        {
            List<MenuItem> menuItems = new List<MenuItem>()
            {
                new MenuItem()
                {
                    Id = 1,
                    Name = "Veggie Burger",
                    Description = "Delicious plant-based burger",
                    Price = 12.99m,
                    ImageUrl = "/images/veggieBurger.jpg",
                    RestaurantId = 1
                },
                new MenuItem()
                {
                    Id = 2,
                    Name = "Beef Burger",
                    Description = "Delicious burger with fresh meat",
                    Price = 18.99m,
                    ImageUrl = "/images/beefBurger.jpg",
                    RestaurantId = 1
                },
                new MenuItem()
                {
                    Id = 3,
                    Name = "Chicken Burger",
                    Description = "Delicious burger with chicken and cheese",
                    Price = 15.99m,
                    ImageUrl = "/images/chickenBurger.jpg",
                    RestaurantId = 1
                },
                new MenuItem()
                {
                    Id = 4,
                    Name = "Spaghetti Bolognese",
                    Description = "Classic Italian pasta with rich sauce",
                    Price = 16.50m,
                    ImageUrl = "/images/meatPasta.jpg",
                    RestaurantId = 2
                },
                new MenuItem()
                {
                    Id = 5,
                    Name = "Margherita Pizza",
                    Description = "Classic pizza with tomato, basil, and mozzarella",
                    Price = 11.99m,
                    ImageUrl = "/images/pizzaM.jpg",
                    RestaurantId = 2
                },
                new MenuItem()
                {
                    Id = 6,
                    Name = "Caesar Salad",
                    Description = "Fresh lettuce with Caesar dressing",
                    Price = 10.99m,
                    ImageUrl = "/images/ceaserSalad.jpg",
                    RestaurantId = 2
                },
                new MenuItem()
                {
                    Id = 7,
                    Name = "Cheesecake",
                    Description = "Our best seller",
                    Price = 8.99m,
                    ImageUrl = "/images/cheesecake.jpg",
                    RestaurantId = 3
                },
                new MenuItem()
                {
                    Id = 8,
                    Name = "Chocolate cake",
                    Description = "The richest chocolate taste you can find",
                    Price = 7.99m,
                    ImageUrl = "/images/chokoCake.jpg",
                    RestaurantId = 3
                },
                new MenuItem()
                {
                    Id = 9,
                    Name = "Cream Brulee",
                    Description = "Tastiest cream brulee you can find",
                    Price = 5.99m,
                    ImageUrl = "/images/creamB.jpg",
                    RestaurantId = 3
                }
            };
            return menuItems;
        }
    }
}
