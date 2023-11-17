using Microsoft.AspNetCore.Mvc;

namespace Gym.Web.Areas.Customer.Controllers
{
    public class DangNhap : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
