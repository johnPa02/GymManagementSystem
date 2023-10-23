using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gym.Models;

namespace Gym.ViewModels
{
	public class PackageViewModel
	{
		public int PackageId { get; set; }
		public string PackageName { get; set; }
		public decimal Price { get; set; }
		public int Duration { get; set; }
		public string Description { get; set; }
		public bool IsActive { get; set; }
		public PackageViewModel()
		{

		}
		public PackageViewModel(Package model)
		{
			PackageId= model.PackageId;
			PackageName= model.PackageName;
			Price= model.Price;
			Duration= model.Duration;
			Description= model.Description;
			IsActive = model.IsActive;
		}
		public Package ConvertPackageModel(PackageViewModel model) 
		{
			return new Package
			{
				PackageId = model.PackageId,
				PackageName = model.PackageName,
				Price = model.Price,
				Duration = model.Duration,
				Description = model.Description,
				IsActive = model.IsActive
			};
		}
	}
}
