using Microsoft.AspNetCore.Mvc;

namespace TastyOrders.Web.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
