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
		public int Price { get; set; }
		public int Duration { get; set; }
		public string Description { get; set; }
		public bool IsActive { get; set; }
        public string TimeSlot { get; set; }
        public bool UnlimitedAccess { get; set; }
        public IEnumerable<string> AvailableTimeSlots { get; set; }


        public PackageViewModel()
		{
            AvailableTimeSlots = new List<string> { "Morning (8 AM - 4 PM)", "Evening (4 PM - 10 PM)" };

        }
        public PackageViewModel(Package model)
		{
			PackageId= model.PackageId;
			PackageName= model.PackageName;
			Price= model.Price;
			Duration= model.Duration;
			Description= model.Description;
			IsActive = model.IsActive;
			TimeSlot = model.TimeSlot;
			UnlimitedAccess = model.UnlimitedAccess;
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
				IsActive = model.IsActive,
                TimeSlot = model.TimeSlot,
				UnlimitedAccess = model.UnlimitedAccess
			};
		}
	}
}
