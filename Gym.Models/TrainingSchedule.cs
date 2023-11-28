using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Models
{
	public class TrainingSchedule
	{
		[Key]
		public int ScheduleId { get; set; }
        public int TrainerId { get; set; }
        public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public string Location { get; set; }
		public string Description { get; set; }
        public bool IsSpecialClass { get; set; }
        public ApplicationUser Trainer { get; set; }
	}
}
