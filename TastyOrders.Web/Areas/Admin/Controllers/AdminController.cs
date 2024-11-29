using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TastyOrders.Services.Data.Interfaces;

namespace TastyOrders.Web.Areas.Admin.Controllers
{
    using static Common.ApplicationConstants;
    using static Common.ErrorMessages.Admin;

    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> ManageRoles()
        {
            var model = await adminService.GetUsersWithRolesAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string role)
        {
            var success = await adminService.AssignRoleToUserAsync(userId, role);

            if (!success)
            {
                TempData[ErrorMessage] = FailedToAssignRoleMessage;
                return RedirectToAction(nameof(ManageRoles));
            }

            TempData[SuccessMessage] = $"Role '{role}' has been assigned successfully.";
            return RedirectToAction(nameof(ManageRoles));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(string userId, string role)
        {
            var success = await adminService.RemoveRoleFromUserAsync(userId, role);

            if (!success)
            {
                TempData[ErrorMessage] = FailedToRemoveRoleMessage;
                return RedirectToAction(nameof(ManageRoles));
            }

            TempData[SuccessMessage] = $"Role '{role}' has been removed successfully.";
            return RedirectToAction(nameof(ManageRoles));
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(string email, string password)
        {
            var (success, message) = await adminService.AddUserAsync(email, password);

            if (!success)
            {
                TempData[ErrorMessage] = message;
                return RedirectToAction(nameof(ManageRoles));
            }

            TempData[SuccessMessage] = message;
            return RedirectToAction(nameof(ManageRoles));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUser(string userId)
        {
            var (success, message) = await adminService.RemoveUserAsync(userId);

            if (!success)
            {
                TempData[ErrorMessage] = message;
                return RedirectToAction(nameof(ManageRoles));
            }

            TempData[SuccessMessage] = message;
            return RedirectToAction(nameof(ManageRoles));
        }
    }
}
