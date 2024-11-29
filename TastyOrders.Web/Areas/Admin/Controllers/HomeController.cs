using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static TastyOrders.Common.ApplicationConstants;

namespace TastyOrders.Web.Areas.Admin.Controllers
{
    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]
    public class HomeController : Controller
    {   
        public IActionResult Index()
        {
            return View();
        }
    }
}
