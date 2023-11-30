using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Models
{
	public class Invoice
	{
		public int InvoiceId { get; set; }
		public int TotalAmount { get; set; }
		public DateTime Date { get; set; }
		public string Status { get; set; }

		public ApplicationUser Customer { get; set; }
		public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
	}
}
