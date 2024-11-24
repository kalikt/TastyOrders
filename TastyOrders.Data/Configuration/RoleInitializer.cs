using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyOrders.Data.Models;

namespace TastyOrders.Data.Configuration
{
    using static Common.ApplicationConstants;
    public static class RoleInitializer
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Seed Admin Role
            if (!await roleManager.RoleExistsAsync(AdminRoleName))
            {
                await roleManager.CreateAsync(new IdentityRole(AdminRoleName));
            }

            // Seed Admin User
            var adminEmail = AdminEmail;
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, AdminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, AdminRoleName);
                }
            }
        }
    }
}
