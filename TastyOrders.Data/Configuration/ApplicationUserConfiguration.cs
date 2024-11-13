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
    using static TastyOrders.Common.EntityValidationConstants.ApplicationUser;
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(FirstNameMaxLength);

            builder
                .Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(LastNameMaxLength);

            builder.HasOne(u => u.Cart)
                  .WithOne(c => c.User)
                  .HasForeignKey<Cart>(c => c.UserId);
        }
    }
}
