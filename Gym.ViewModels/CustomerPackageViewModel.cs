using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gym.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gym.ViewModels
{
	public class CustomerPackageViewModel
	{
		public DateTime StartDate { get; set; } = DateTime.Today;
        public DateTime EndDate { get; set; } = DateTime.Today;

        public int PackageId { get; set; }
		public string CustomerId { get; set; }
        public IEnumerable<SelectListItem> Packages { get; set; }
        public int Id { get; set; }
		public string Email { get; set; }
		public CustomerPackageViewModel()
		{
            Packages = new List<SelectListItem>();
        }
		public CustomerPackageViewModel(CustomerPackage customerPackage)
		{
			Id = customerPackage.Id;
			StartDate= customerPackage.StartDate;
			EndDate= customerPackage.EndDate;
			PackageId = customerPackage.PackageId;
			CustomerId = customerPackage.CustomerId;
		}
		public CustomerPackage ConvertCustomerPackageModel(CustomerPackageViewModel model)
		{
			return new CustomerPackage()
			{
				Id = model.Id,
				PackageId = model.PackageId,
				StartDate = model.StartDate,
				EndDate = model.EndDate,
				CustomerId = model.CustomerId
			};
		}

	}
}
