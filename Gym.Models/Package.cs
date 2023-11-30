using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Models
{
	public class Package
	{
		public int PackageId { get; set; }
		public string PackageName { get; set; }
		public int Price { get; set; }
		public int Duration { get; set; }
		public string Description { get; set; }
		public bool IsActive { get; set; }
        public string TimeSlot { get; set; } // Khung giờ active của gói tập
        public bool UnlimitedAccess { get; set; } // Quyền truy cập không giới hạn
        public bool SpecialClassesIncluded { get; set; }
    }
}
