using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Models
{
	public class CustomerPackage
	{
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public ApplicationUser Customer { get; set; }
		public Package Package { get; set; }
	}
}
