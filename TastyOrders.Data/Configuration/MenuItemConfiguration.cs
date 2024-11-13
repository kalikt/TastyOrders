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

        }

        //private List<MenuItem> SeedMenuItems()
        //{
        //    List<MenuItem> menuItems = new List<MenuItem>()
        //    {
        //        new MenuItem()
        //        {
        //            Name = "Veggie Burger",
        //            Description = "Delicious plant-based burger",
        //            Price = 12.99m,

        //        }
        //    }
        //}
    }
}
