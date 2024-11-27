using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyOrders.Web.ViewModels.Admin;

namespace TastyOrders.Services.Data.Interfaces
{
    public interface IAdminService
    {
        Task<List<UserRoleViewModel>> GetUsersWithRolesAsync();
        Task<bool> AssignRoleToUserAsync(string userId, string role);
        Task<bool> RemoveRoleFromUserAsync(string userId, string role);
        Task<(bool Success, string Message)> AddUserAsync(string email, string password);
        Task<(bool Success, string Message)> RemoveUserAsync(string userId);
    }
}
