using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gym.Models;
using Gym.Repositories.Interfaces;
using Gym.Utillities;
using Gym.ViewModels;

namespace Gym.Services
{
	public class CustomerPackageService : ICustomerPackageService
	{
		private readonly IUnitOfWork _unitOfWork;
		public CustomerPackageService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public void DeleteCustomerPackage(int id)
		{
			var model = _unitOfWork.GenericRepository<CustomerPackage>().GetById(id);
			_unitOfWork.GenericRepository<CustomerPackage>().Delete(model);
			_unitOfWork.Save();
		}

		public PagedResult<CustomerPackageViewModel> GetAll(int pageNumber, int pageSize)
		{
			var vm = new CustomerPackageViewModel();
			int totalCount;
			List<CustomerPackageViewModel> vmList = new List<CustomerPackageViewModel>();
			try
			{
				int ExcludeRecords = pageNumber * pageSize - pageSize;
				var modelList = _unitOfWork.GenericRepository<CustomerPackage>().GetAll().Skip(ExcludeRecords)
					.Take(pageSize).ToList();
				totalCount = _unitOfWork.GenericRepository<CustomerPackage>().GetAll().ToList().Count();
				vmList = ConvertModelToViewModelList(modelList);
			}
			catch (Exception)
			{
				throw;
			}
			var result = new PagedResult<CustomerPackageViewModel>
			{
				Data = vmList,
				TotalItems = totalCount,
				PageNumber = pageNumber,
				PageSize = pageSize
			};
			return result;
		}

		public CustomerPackageViewModel GetCustomerPackageById(int id)
		{
			var model = _unitOfWork.GenericRepository<CustomerPackage>().GetById(id);
			var vm = new CustomerPackageViewModel(model);
			return vm;
		}

		public void InsertCustomerPackage(CustomerPackageViewModel customerPackage)
		{
			var model = new CustomerPackageViewModel().ConvertCustomerPackageModel(customerPackage);
			_unitOfWork.GenericRepository<CustomerPackage>().Add(model);
			_unitOfWork.Save();
		}

		public void UpdateCustomerPackage(CustomerPackageViewModel customerPackage)
		{
			var model = new CustomerPackageViewModel().ConvertCustomerPackageModel(customerPackage);
			var ModelById = _unitOfWork.GenericRepository<CustomerPackage>().GetById(model.Id);
			ModelById.StartDate = customerPackage.StartDate;
			ModelById.EndDate = customerPackage.EndDate;

			_unitOfWork.GenericRepository<CustomerPackage>().Update(ModelById);
			_unitOfWork.Save();
		}
		public List<CustomerPackageViewModel> ConvertModelToViewModelList(List<CustomerPackage> packages)
		{
			return packages.Select(x => new CustomerPackageViewModel(x)).ToList();
		}
	}
}
