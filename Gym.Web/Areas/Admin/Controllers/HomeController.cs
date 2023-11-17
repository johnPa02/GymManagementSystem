using Microsoft.AspNetCore.Mvc;

namespace Gym.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class HomeController : Controller
    {
        [Route("/Admin/Home")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
