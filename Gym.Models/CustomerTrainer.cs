using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Models
{
	public class CustomerTrainer
	{
		public int Id { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string ProgressNotes { get; set; }

		public ApplicationUser Customer { get; set; }
		public ApplicationUser Trainer { get; set; }
	}
}
