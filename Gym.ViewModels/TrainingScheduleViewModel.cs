using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gym.Models;

namespace Gym.ViewModels
{
    public class TrainingScheduleViewModel
    {
        public int TrainingScheduleId { get; set; }
        public int TrainerId { get; set; }
        public string TrainerName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [Required(ErrorMessage = "Không được để trống vị trí")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Không được để trống mô tả")]
        public string Description { get; set; }
        public bool IsSpecialClass { get; set; }
        public string TimeSlot { get; set; }
        public IEnumerable<string> AvailableTimeSlots { get; set; }

        public TrainingScheduleViewModel()
        {
            AvailableTimeSlots = new List<string> { "Morning (8 AM - 4 PM)", "Evening (4 PM - 10 PM)" };
        }

        public TrainingScheduleViewModel(TrainingSchedule model)
        {
            TrainingScheduleId = model.ScheduleId;
            TrainerId = model.TrainerId;
            StartTime = model.StartTime;
            EndTime = model.EndTime;
            Location = model.Location;
            Description = model.Description;
            IsSpecialClass = model.IsSpecialClass;
            TimeSlot = model.TimeSlot;
        }

        public TrainingSchedule ConvertToModel()
        {
            return new TrainingSchedule
            {
                ScheduleId = this.TrainingScheduleId,
                TrainerId = this.TrainerId,
                StartTime = this.StartTime,
                EndTime = this.EndTime,
                Location = this.Location,
                Description = this.Description,
                IsSpecialClass = this.IsSpecialClass,
                TimeSlot = this.TimeSlot
            };
        }
    }
}
