using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TastyOrders.Data.Models;

namespace TastyOrders.Data.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder
                .HasKey(oi => oi.Id);

            builder
                .Property(oi => oi.Quantity)
                .IsRequired();

            builder
                .Property(t => t.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasOne(oi => oi.Order)
                   .WithMany(o => o.OrderItems)
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(oi => oi.MenuItem)
                   .WithMany()
                   .HasForeignKey(oi => oi.MenuItemId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
