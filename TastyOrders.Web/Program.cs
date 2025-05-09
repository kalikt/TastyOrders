using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TastyOrders.Data;

namespace TastyOrders
{
    using TastyOrders.Data.Configuration;
    using TastyOrders.Data.Models;
    using TastyOrders.Services.Data.Interfaces;
    using TastyOrders.Web.Infrastructure.Extensions;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("SqlServer") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<TastyOrdersDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                ConfigureIdentity(builder, options);
            })
                .AddEntityFrameworkStores<TastyOrdersDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.RegisterUserDefinedServices(typeof(IAdminService).Assembly);

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;   
                RoleInitializer.SeedRolesAndAdminAsync(services).GetAwaiter().GetResult();
            }

            app.MapControllerRoute(
            name: "Areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "Errors",
                pattern: "{controller=Home}/{action=Index}/{statusCode?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }

        private static void ConfigureIdentity(WebApplicationBuilder builder, IdentityOptions options)
        {
            options.Password.RequireDigit = builder.Configuration
                .GetValue<bool>("Identity:Password:RequireDigit");
            options.Password.RequireLowercase = builder.Configuration
            .GetValue<bool>("Identity:Password:RequireLowercase");
            options.Password.RequireUppercase = builder.Configuration
            .GetValue<bool>("Identity:Password:RequireUppercase");
            options.Password.RequireNonAlphanumeric = builder.Configuration
            .GetValue<bool>("Identity:Password:RequireNonAplhanumerical");
            options.Password.RequiredLength = builder.Configuration
            .GetValue<int>("Identity:Password:RequireLength");
            options.Password.RequiredUniqueChars = builder.Configuration
            .GetValue<int>("Identity:Password:RequireUniqueCharacters");

            options.SignIn.RequireConfirmedAccount = builder.Configuration
            .GetValue<bool>("Identity:Password:RequireConfirmedAccount");
            options.SignIn.RequireConfirmedEmail = builder.Configuration
            .GetValue<bool>("Identity:Password:RequireConfirmedEmail");
            options.SignIn.RequireConfirmedPhoneNumber = builder.Configuration
            .GetValue<bool>("Identity:Password:RequireConfirmedPhoneNumber");

            options.User.RequireUniqueEmail = builder.Configuration
            .GetValue<bool>("Identity:Password:RequireUniqueEmail");
        }
    }
}
