using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Models
{
	public class CustomerPackage
	{
		public int Id { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public ApplicationUser Customer { get; set; }
		public Package Package { get; set; }
		public int PackageId { get; set; }
		public int CustomerId { get; set; }
	}
}
