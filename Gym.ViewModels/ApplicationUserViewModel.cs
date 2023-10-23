using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gym.Models;

namespace Gym.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public string Specialization { get; set; }
        public string Role { get; set; }

        public ApplicationUserViewModel()
        {
        }

        public ApplicationUserViewModel(ApplicationUser model)
        {
            UserName = model.UserName;
            FullName = model.FullName;
            DateOfBirth = model.DateOfBirth;
            PhoneNumber = model.PhoneNumber;
            IsActive = model.IsActive;
            Specialization = model.Specialization;
        }

        public ApplicationUser ConvertToModel()
        {
            return new ApplicationUser
            {
                UserName = this.UserName,
                FullName = this.FullName,
                DateOfBirth = this.DateOfBirth,
                PhoneNumber = this.PhoneNumber,
                IsActive = this.IsActive,
                Specialization = this.Specialization
            };
        }
    }
}
