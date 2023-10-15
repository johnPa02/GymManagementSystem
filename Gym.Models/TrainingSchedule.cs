﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Models
{
	public class TrainingSchedule
	{
		public int ScheduleId { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public string Location { get; set; }
		public string Description { get; set; }

		public ApplicationUser Trainer { get; set; }
	}
}
