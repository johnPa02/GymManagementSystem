using Microsoft.AspNetCore.Mvc;

namespace Gym.Web.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class CustomersController : Controller
	{
		[Route("/")]
		public IActionResult CustomerHome()
		{
			return View();
		}


	}
}
