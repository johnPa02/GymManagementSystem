using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gym.Utillities;
using Gym.ViewModels;

namespace Gym.Services
{
	public interface ICustomerPackageService
	{
		PagedResult<CustomerPackageViewModel> GetAll(int pageNumber, int pageSize);
		CustomerPackageViewModel GetCustomerPackageById(int id);
		void UpdateCustomerPackage(CustomerPackageViewModel customerPackage);
		void DeleteCustomerPackage(int id);
		void InsertCustomerPackage(CustomerPackageViewModel customerPackage);
	}
}
