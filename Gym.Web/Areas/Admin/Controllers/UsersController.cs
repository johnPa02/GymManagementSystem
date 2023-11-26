using System.Data;
using Gym.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Gym.ViewModels;
using Gym.Models;

namespace Gym.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private IApplicationUserService _userService;

        public UsersController(IApplicationUserService userService)
        {
            _userService = userService;
        }
        [Route("/Admin/UserManagement")]
        public IActionResult Index(int PageNumber=1, int PageSize=10)
        {
            return View(_userService.GetUsersByRole(PageNumber, PageSize, "Customer"));
        }

        [Route("/Admin/TrainerManagement")]
        public IActionResult AllTrainers(int PageNumber = 1, int PageSize = 10)
        {
            return View(_userService.GetUsersByRole(PageNumber, PageSize,"Trainer"));
        }

        public IActionResult Search(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            var model = _userService.SearchUsers(searchTerm, pageNumber, pageSize);
            return View("Index",model);
        }
        public IActionResult Delete(string id)
        {
            _userService.DeleteUser(id);
            // Lấy URL của trang hiện tại
            var returnUrl = Request.Headers["Referer"].ToString();

            // Kiểm tra nếu URL không rỗng và hợp lệ, sau đó chuyển hướng tới URL đó
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var model = _userService.GetUserById(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(ApplicationUserViewModel userViewModel)
        {
            _userService.UpdateUser(userViewModel);
            return RedirectToAction("Index");
        }
    }
}
