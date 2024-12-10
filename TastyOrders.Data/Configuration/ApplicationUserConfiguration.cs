using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TastyOrders.Data.Models;

namespace TastyOrders.Data.Configuration
{
    using static TastyOrders.Common.EntityValidationConstants.ApplicationUser;
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {

            builder.HasOne(u => u.Cart)
       .WithOne(c => c.User)
       .HasForeignKey<Cart>(c => c.UserId)
       .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
