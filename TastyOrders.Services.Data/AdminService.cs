using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TastyOrders.Data.Models;
using TastyOrders.Services.Data.Interfaces;
using TastyOrders.Web.ViewModels.Admin;

namespace TastyOrders.Services.Data
{
    using static Common.EntityValidationMessages.Admin;
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<List<UserRoleViewModel>> GetUsersWithRolesAsync()
        {
            var users = await userManager.Users.ToListAsync();
            var roles = await roleManager.Roles.Select(r => r.Name).ToListAsync();

            return users.Select(user => new UserRoleViewModel
            {
                UserId = user.Id,
                UserName = user.UserName ?? string.Empty,
                Roles = userManager.GetRolesAsync(user).Result.ToList(),
                AllRoles = roles
            }).ToList();
        }

        public async Task<bool> AssignRoleToUserAsync(string userId, string role)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null || !await roleManager.RoleExistsAsync(role))
            {
                return false;
            }

            var result = await userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }

        public async Task<bool> RemoveRoleFromUserAsync(string userId, string role)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            var result = await userManager.RemoveFromRoleAsync(user, role);
            return result.Succeeded;
        }

        public async Task<(bool Success, string Message)> AddUserAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return (false, EmailAndPasswordRequiredMessage);
            }

            var newUser = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            var result = await userManager.CreateAsync(newUser, password);

            if (!result.Succeeded)
            {
                var errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
                return (false, errorMessage);
            }

            return (true, SuccessfullyAddedUserMessage);
        }

        public async Task<(bool Success, string Message)> RemoveUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return (false, UserNotFoundMessage);
            }

            var result = await userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return (false, FailedRemovalOfUserMessage);
            }

            return (true, $"User '{user.UserName}' has been removed successfully.");
        }
    }
}
