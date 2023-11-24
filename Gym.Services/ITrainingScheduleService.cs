using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gym.ViewModels;
using Gym.Utillities;

namespace Gym.Services
{
    public interface ITrainingScheduleService
    {
        PagedResult<TrainingScheduleViewModel> GetAll(int pageNumber, int pageSize);
        IEnumerable<TrainingScheduleViewModel> GetAllSchedules();
        IEnumerable<TrainingScheduleViewModel> GetSchedulesByWeek(DateTime startOfWeek, DateTime endOfWeek);
        TrainingScheduleViewModel GetScheduleById(int id);
        void UpdateSchedule(TrainingScheduleViewModel schedule);
        void DeleteSchedule(int id);
        void InsertSchedule(TrainingScheduleViewModel schedule);
    }
}
