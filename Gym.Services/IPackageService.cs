using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Gym.ViewModels;
using Gym.Utillities;

namespace Gym.Services
{
	public interface IPackageService
	{
		PagedResult<PackageViewModel> GetAll(int pageNumber, int pageSize);
		PackageViewModel GetPackageById(int id);
		void UpdatePackage(PackageViewModel package);
		void DeletePackage(int id);
		void InsertPackage(PackageViewModel package);
	}
}
