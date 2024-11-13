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
        }
    }
}
