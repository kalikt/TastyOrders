using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TastyOrders.Data.Models;

namespace TastyOrders.Data.Configuration
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder
                .HasKey(ci => ci.Id);

            builder
               .Property(oi => oi.Quantity)
               .IsRequired();

            builder
                .Property(t => t.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasOne(ci => ci.MenuItem)
                   .WithMany()
                   .HasForeignKey(ci => ci.MenuItemId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ci => ci.Cart)
                  .WithMany(cart => cart.CartItems)
                  .HasForeignKey(ci => ci.CartId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
