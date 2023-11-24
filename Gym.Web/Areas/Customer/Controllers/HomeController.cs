using Gym.Models;
using Gym.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Web.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
	{
        private readonly ITrainingScheduleService _trainingScheduleService;

        public HomeController(ITrainingScheduleService trainingScheduleService)
        {
            _trainingScheduleService = trainingScheduleService;
        }

        [Route("/")]
        public IActionResult Index()
		{
			return View();
		}
        public IActionResult TrainingSchedule(DateTime? date)
        {
            var chosenDate = date ?? DateTime.Today;
            var startOfWeek = chosenDate.AddDays(-(int)chosenDate.DayOfWeek + (int)DayOfWeek.Monday);
            var endOfWeek = startOfWeek.AddDays(6);

            var weeklySchedules = _trainingScheduleService.GetSchedulesByWeek(startOfWeek, endOfWeek);

            ViewBag.StartOfWeek = startOfWeek;
            ViewBag.EndOfWeek = endOfWeek;

            return View(weeklySchedules);
        }
    }
}
