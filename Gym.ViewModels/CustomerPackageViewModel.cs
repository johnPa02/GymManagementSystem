using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gym.Models;

namespace Gym.ViewModels
{
	public class CustomerPackageViewModel
	{
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public int PackageId { get; set; }
		public int CustomerId { get; set; }
		public int Id { get; set; }
		public CustomerPackageViewModel()
		{

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
