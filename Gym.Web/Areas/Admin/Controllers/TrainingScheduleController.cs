using System.Data;
using Gym.Services;
using Gym.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TrainingScheduleController : Controller
    {
        private readonly ITrainingScheduleService _trainingScheduleService;
        private readonly IApplicationUserService _applicationUserService;

        public TrainingScheduleController(ITrainingScheduleService trainingScheduleService, IApplicationUserService applicationUserService)
        {
            _trainingScheduleService = trainingScheduleService;
            _applicationUserService = applicationUserService;
        }
        [Route("/Admin/TrainingScheduleManagement")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var schedules = _trainingScheduleService.GetAll(pageNumber, pageSize);
            return View(schedules);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new TrainingScheduleViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrainingScheduleViewModel viewModel)
        {
            ModelState.Remove("TrainerName");
            var trainer = _applicationUserService.GetUserById(viewModel.TrainerId);
            if (trainer == null)
            {
                ModelState.AddModelError("TrainerId", "Trainer ID khong ton tai");
            }
            if (viewModel.StartTime >= viewModel.EndTime)
            {
                ModelState.AddModelError("", "Thời gian bắt đầu phải nhỏ hơn kết thúc.");
            }
            if (ModelState.IsValid)
            {
                _trainingScheduleService.InsertSchedule(viewModel, ModelState);
                if (ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var schedule = _trainingScheduleService.GetScheduleById(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return View(schedule);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TrainingScheduleViewModel viewModel)
        {
            if (id != viewModel.TrainingScheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _trainingScheduleService.UpdateSchedule(viewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            _trainingScheduleService.DeleteSchedule(id);
            return RedirectToAction("Index");
        }
    }
}
