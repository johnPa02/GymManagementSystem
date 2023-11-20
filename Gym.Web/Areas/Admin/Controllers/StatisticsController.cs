using Microsoft.AspNetCore.Mvc;

namespace Gym.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatisticsController : Controller
    {
        [Route("/Admin/Statistics")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
