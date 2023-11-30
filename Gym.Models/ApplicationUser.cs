using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Gym.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string FullName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public bool IsActive { get; set; }
		public string Specialization { get; set; }

        public ICollection<TrainingSchedule> TrainingSchedules { get; set; }
		[NotMapped]
		public ICollection<CustomerTrainer> CustomerTrainers { get; set; }
		public ICollection<Invoice> Invoices { get; set; }
		public ICollection<CustomerPackage> CustomerPackages { get; set; }
	}
}
