using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyOrders.Data;

namespace TastyOrders.Web.Infrastructure.Extensions
{
    public static class ExtensionMethods
    { 
        //public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
        //{
        //    using IServiceScope serviceScope = app.ApplicationServices.CreateScope();
        //    TastyOrdersDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<TastyOrdersDbContext>()!;
        //    dbContext.Database.Migrate();

        //    return app;
        //}
    }
}
