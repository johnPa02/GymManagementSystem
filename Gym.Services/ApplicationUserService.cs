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
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public ApplicationUserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }


        public PagedResult<ApplicationUserViewModel> GetAll(int PageNumber, int PageSize)
        {
            int totalCount;
            List<ApplicationUserViewModel> vmList = new List<ApplicationUserViewModel>();
            try
            {
                int ExcludeRecords = (PageSize * PageNumber) - PageSize;
                var modelList = _unitOfWork.GenericRepository<ApplicationUser>().GetAll().Skip(ExcludeRecords).Take(PageSize).ToList();
                totalCount = _unitOfWork.GenericRepository<ApplicationUser>().GetAll().ToList().Count;
                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {

                throw;
            }
            var result = new PagedResult<ApplicationUserViewModel>
            {
                Data= vmList,
                TotalItems= totalCount,
                PageNumber=PageNumber,
                PageSize=PageSize
            };
            return result;
        }

        public PagedResult<ApplicationUserViewModel> GetAllCustomer(int PageNumber, int PageSize)
        {
            throw new NotImplementedException();
        }

        public PagedResult<ApplicationUserViewModel> GetAllReceptionist(int PageNumber, int PageSize)
        {
            throw new NotImplementedException();
        }

        public PagedResult<ApplicationUserViewModel> GetAllTrainer(int pageNumber, int pageSize)
        {
            var trainers = new List<ApplicationUser>();
            var trainerRole = _roleManager.FindByNameAsync("Trainer").GetAwaiter().GetResult();

            if (trainerRole != null)
            {
                var userIds = _userManager.GetUsersInRoleAsync(trainerRole.Name).Result.Select(u => u.Id).ToList();
                trainers = _unitOfWork.GenericRepository<ApplicationUser>().GetAll()
                            .Where(u => userIds.Contains(u.Id)).ToList();
            }

            int totalCount = trainers.Count;
            int excludeRecords = (pageSize * pageNumber) - pageSize;
            var trainerList = trainers.Skip(excludeRecords).Take(pageSize).ToList();
            var trainerViewModelList = ConvertModelToViewModelList(trainerList);

            var result = new PagedResult<ApplicationUserViewModel>
            {
                Data = trainerViewModelList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return result;
        }

        public PagedResult<ApplicationUserViewModel> SearchTrainer(int PageNumber, int PageSize, string Spicility)
        {
            throw new NotImplementedException();
        }
        private List<ApplicationUserViewModel> ConvertModelToViewModelList(List<ApplicationUser> modelList)
        {
            var viewModelList = new List<ApplicationUserViewModel>();

            foreach (var user in modelList)
            {
                var roles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult();
                var role = roles.FirstOrDefault();
                var userViewModel = new ApplicationUserViewModel(user) { Role = role };
                viewModelList.Add(userViewModel);
            }

            return viewModelList;
        }
        public PagedResult<ApplicationUserViewModel> SearchUsers(string searchTerm, int pageNumber, int pageSize)
        {
            searchTerm = searchTerm?.ToLower() ?? "";
            var query = _unitOfWork.GenericRepository<ApplicationUser>().GetAll();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(u => u.UserName.ToLower().Contains(searchTerm) ||
                                         u.FullName.ToLower().Contains(searchTerm) ||
                                         u.Email.ToLower().Contains(searchTerm));
            }

            var totalCount = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var viewModelList = ConvertModelToViewModelList(items);

            return new PagedResult<ApplicationUserViewModel>
            {
                Data = viewModelList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
