using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gym.Models;
using Gym.Repositories.Implementation;
using Gym.Repositories.Interfaces;
using Gym.Utillities;
using Gym.ViewModels;

namespace Gym.Services
{
	public class PackageService : IPackageService
	{
		private readonly IUnitOfWork _unitOfWork;
		public PackageService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public void DeletePackage(int id)
		{
			var model = _unitOfWork.GenericRepository<Package>().GetById(id);
			_unitOfWork.GenericRepository<Package>().Delete(model);
			_unitOfWork.Save();
		}
		public PagedResult<PackageViewModel> GetAll(int pageNumber, int pageSize)
		{
			var vm = new PackageViewModel();
			int totalCount;
			List<PackageViewModel> vmList = new List<PackageViewModel>();
			try
			{
				int ExcludeRecords = pageNumber*pageSize - pageSize;
				var modelList = _unitOfWork.GenericRepository<Package>().GetAll().Skip(ExcludeRecords)
					.Take(pageSize).ToList();
				totalCount= _unitOfWork.GenericRepository<Package>().GetAll().ToList().Count();
				vmList = ConvertModelToViewModelList(modelList);
			}
			catch(Exception) 
			{
				throw;
			}
			var result = new PagedResult<PackageViewModel>
			{
				Data= vmList,
				TotalItems= totalCount,
				PageNumber=pageNumber,
				PageSize=pageSize
			};
			return result;
		}
		public PackageViewModel GetPackageById(int id)
		{
			var model = _unitOfWork.GenericRepository<Package>().GetById(id);
			var vm = new PackageViewModel(model);
			return vm;
		}
		public List<PackageViewModel> ConvertModelToViewModelList(List<Package> packages) 
		{
			return packages.Select(x => new PackageViewModel(x)).ToList();
		}

		public void UpdatePackage(PackageViewModel package)
		{
			var model = new PackageViewModel().ConvertPackageModel(package);
			var ModelById = _unitOfWork.GenericRepository<Package>().GetById(model.PackageId);
			ModelById.PackageName = package.PackageName;
			ModelById.Price = package.Price;
			ModelById.Description = package.Description;
			ModelById.Duration = package.Duration;
			ModelById.IsActive= package.IsActive;

			_unitOfWork.GenericRepository<Package>().Update(ModelById);
			_unitOfWork.Save();
		}

		public void InsertPackage(PackageViewModel package)
		{
			var model = new PackageViewModel().ConvertPackageModel(package);
			_unitOfWork.GenericRepository<Package>().Add(model);
			_unitOfWork.Save();
		}
        public List<PackageViewModel> GetAllPackages()
        {
            var modelList = _unitOfWork.GenericRepository<Package>().GetAll().ToList();
            var vmList = ConvertModelToViewModelList(modelList);
            return vmList;
        }
    }
}
