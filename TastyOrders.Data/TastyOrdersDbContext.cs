using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TastyOrders.Data;

public class TastyOrdersDbContext : IdentityDbContext<IdentityUser>
{
    public TastyOrdersDbContext()
    {

    }
    public TastyOrdersDbContext(DbContextOptions<TastyOrdersDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
