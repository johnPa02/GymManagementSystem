using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Models
{
	public class InvoiceDetail
	{
		public int InvoiceDetailId { get; set; }
		public int PricePerUnit { get; set; }
		public int Quantity { get; set; }
		public int Amount { get; set; }

		public Invoice Invoice { get; set; }
		public Service Service { get; set; }
	}
}
