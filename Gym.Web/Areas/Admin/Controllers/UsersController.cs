﻿using Gym.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Gym.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            return View(_userService.GetAll(PageNumber, PageSize));
        }

        [Route("/Admin/TrainerManagement")]
        public IActionResult AllTrainers(int PageNumber = 1, int PageSize = 10)
        {
            return View(_userService.GetAllTrainer(PageNumber, PageSize));
        }

        public IActionResult Search(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            var model = _userService.SearchUsers(searchTerm, pageNumber, pageSize);
            return View("Index",model);
        }
    }
}
