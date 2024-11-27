using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TastyOrders.Data.Models;
using TastyOrders.Web.ViewModels.Admin;

namespace TastyOrders.Web.Areas.Admin.Controllers
{
    using static Common.ApplicationConstants;

    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> ManageRoles()
        {
            var users = await userManager.Users.ToListAsync();
            var roles = await roleManager.Roles.Select(r => r.Name).ToListAsync();

            var model = users.Select(user => new UserRoleViewModel
            {
                UserId = user.Id,
                UserName = user.UserName ?? string.Empty,
                Roles = userManager.GetRolesAsync(user).Result.ToList(),
                AllRoles = roles
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string role)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                return BadRequest("Role does not exist.");
            }

            var result = await userManager.AddToRoleAsync(user, role);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to assign role.");
                return RedirectToAction(nameof(ManageRoles));
            }

            TempData["SuccessMessage"] = $"Role '{role}' has been assigned to user '{user.UserName}'.";
            return RedirectToAction(nameof(ManageRoles));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(string userId, string role)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var result = await userManager.RemoveFromRoleAsync(user, role);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove role.");
                return RedirectToAction(nameof(ManageRoles));
            }

            TempData["SuccessMessage"] = $"Role '{role}' has been removed from user '{user.UserName}'.";
            return RedirectToAction(nameof(ManageRoles));
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                TempData["ErrorMessage"] = "Email and password are required.";
                return RedirectToAction(nameof(ManageRoles));
            }

            var newUser = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            var result = await userManager.CreateAsync(newUser, password);

            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = "Failed to add user: " + string.Join(", ", result.Errors.Select(e => e.Description));
                return RedirectToAction(nameof(ManageRoles));
            }

            TempData["SuccessMessage"] = $"User '{email}' has been added successfully.";
            return RedirectToAction(nameof(ManageRoles));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction(nameof(ManageRoles));
            }

            var result = await userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = "Failed to remove user.";
                return RedirectToAction(nameof(ManageRoles));
            }

            TempData["SuccessMessage"] = $"User '{user.UserName}' has been removed successfully.";
            return RedirectToAction(nameof(ManageRoles));
        }

    }
}
