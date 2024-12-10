using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TastyOrders.Data.Models;

namespace TastyOrders.Data.Configuration
{
    using static TastyOrders.Common.EntityValidationConstants.Review;
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder
                .HasKey(r => r.Id);

            builder
                .Property(r => r.Rating)
                .IsRequired();

            builder
                .Property(r => r.Comment)
                .IsRequired()
                .HasMaxLength(CommentMaxLength);

            builder
                .Property(r => r.CreatedAt)
                .IsRequired();

            builder
                .HasOne(r => r.User)
                   .WithMany()
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(r => r.Restaurant)
                   .WithMany(r => r.Reviews)
                   .HasForeignKey(r => r.RestaurantId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
