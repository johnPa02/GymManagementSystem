using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gym.Models;
using Gym.Repositories.Interfaces;
using Gym.Utillities;
using Gym.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Gym.Services
{
	public class CustomerPackageService : ICustomerPackageService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IApplicationUserService _applicationUser;
        public CustomerPackageService(IUnitOfWork unitOfWork, IApplicationUserService applicationUser)
		{
			_unitOfWork = unitOfWork;
			_applicationUser= applicationUser;
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
        public PagedResult<CustomerPackageViewModel> SearchCustomerPackages(string searchTerm, int pageNumber, int pageSize)
        {
            searchTerm = searchTerm?.ToLower() ?? "";

			var user = _applicationUser.GetUserByEmailAsync(searchTerm).Result;
            var query = _unitOfWork.GenericRepository<CustomerPackage>().GetAll();
			if (user != null)
			{
                query = query.Where(u =>
                    u.CustomerId != null &&u.CustomerId.ToLower().Contains(user.Id)
                );
            }
            else if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(u =>
                    u.CustomerId != null && u.CustomerId.ToLower().Contains(searchTerm)
                );
            }

            var totalCount = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var viewModelList = ConvertModelToViewModelList(items);

            return new PagedResult<CustomerPackageViewModel>
            {
                Data = viewModelList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
