using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TastyOrders.Data.Models;

namespace TastyOrders.Data.Configuration
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder.HasOne(c => c.User)
                   .WithOne(u => u.Cart)
                   .HasForeignKey<Cart>(c => c.UserId);

            builder.HasMany(c => c.CartItems)
                   .WithOne(ci => ci.Cart)
                   .HasForeignKey(ci => ci.CartId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
