using Gym.Models;
using Gym.Repositories.Interfaces;
using Gym.Utillities;
using Gym.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gym.Services
{
    public class TrainingScheduleService : ITrainingScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrainingScheduleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<TrainingScheduleViewModel> GetSchedulesByWeek(DateTime startOfWeek, DateTime endOfWeek)
        {
            var schedules = _unitOfWork.GenericRepository<TrainingSchedule>()
            .GetAll()
            .Where(schedule => schedule.StartTime.Date >= startOfWeek.Date && schedule.StartTime.Date <= endOfWeek.Date)
            .OrderBy(schedule => schedule.StartTime)
            .ToList();

            var trainerIds = schedules.Select(s => s.TrainerId.ToString()).Distinct().ToList();
            var trainers = _unitOfWork.GenericRepository<ApplicationUser>()
                .GetAll(user => trainerIds.Contains(user.Id))
                .ToDictionary(user => user.Id, user => user.FullName);

            var scheduleViewModels = schedules.Select(schedule =>
                new TrainingScheduleViewModel(schedule)
                {
                    TrainerName = trainers.TryGetValue(schedule.TrainerId.ToString(), out var name) ? name : "Unknown"
                }).ToList();

            return scheduleViewModels;
        }
        public IEnumerable<TrainingScheduleViewModel> GetAllSchedules()
        {
            var modelList = _unitOfWork.GenericRepository<TrainingSchedule>()
                .GetAll()
                .ToList();
            var vmList = modelList.Select(schedule => new TrainingScheduleViewModel(schedule)).ToList();

            return vmList;
        }
        public PagedResult<TrainingScheduleViewModel> GetAll(int pageNumber, int pageSize)
        {
            var totalCount = _unitOfWork.GenericRepository<TrainingSchedule>().GetAll().Count();
            var skipAmount = pageSize * (pageNumber - 1);
            var modelList = _unitOfWork.GenericRepository<TrainingSchedule>()
                .GetAll()
                .Skip(skipAmount)
                .Take(pageSize)
                .ToList();
            var vmList = modelList.Select(schedule => new TrainingScheduleViewModel(schedule)).ToList();

            return new PagedResult<TrainingScheduleViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public TrainingScheduleViewModel GetScheduleById(int id)
        {
            var model = _unitOfWork.GenericRepository<TrainingSchedule>().GetById(id);
            return new TrainingScheduleViewModel(model);
        }

        public void UpdateSchedule(TrainingScheduleViewModel viewModel)
        {
            var model = _unitOfWork.GenericRepository<TrainingSchedule>().GetById(viewModel.TrainingScheduleId);
            MapViewModelToModel(viewModel, model);
            _unitOfWork.GenericRepository<TrainingSchedule>().Update(model);
            _unitOfWork.Save();
        }

        public void DeleteSchedule(int id)
        {
            var model = _unitOfWork.GenericRepository<TrainingSchedule>().GetById(id);
            _unitOfWork.GenericRepository<TrainingSchedule>().Delete(model);
            _unitOfWork.Save();
        }

        public void InsertSchedule(TrainingScheduleViewModel viewModel, ModelStateDictionary modelState)
        {
            var model = MapViewModelToModel(viewModel, new TrainingSchedule());
            _unitOfWork.GenericRepository<TrainingSchedule>().Add(model);
            _unitOfWork.Save();
        }

        private TrainingSchedule MapViewModelToModel(TrainingScheduleViewModel viewModel, TrainingSchedule model)
        {
            model.TrainerId = viewModel.TrainerId;
            model.StartTime = viewModel.StartTime;
            model.EndTime = viewModel.EndTime;
            model.Location = viewModel.Location;
            model.Description = viewModel.Description;
            model.IsSpecialClass = viewModel.IsSpecialClass;
            return model;
        }
    }
}
