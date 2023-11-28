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
        private readonly SignInManager<IdentityUser> _signInManager;

        public ApplicationUserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

        public PagedResult<ApplicationUserViewModel> GetUsersByRole(int pageNumber, int pageSize,string role)
        {
            var users = new List<ApplicationUser>();
            var userRole = _roleManager.FindByNameAsync(role).GetAwaiter().GetResult();

            if (userRole != null)
            {
                var userIds = _userManager.GetUsersInRoleAsync(userRole.Name).Result.Select(u => u.Id).ToList();
                users = _unitOfWork.GenericRepository<ApplicationUser>().GetAll()
                            .Where(u => userIds.Contains(u.Id)).ToList();
            }

            int totalCount = users.Count;
            int excludeRecords = (pageSize * pageNumber) - pageSize;
            var userList = users.Skip(excludeRecords).Take(pageSize).ToList();
            var userViewModelList = ConvertModelToViewModelList(userList);

            var result = new PagedResult<ApplicationUserViewModel>
            {
                Data = userViewModelList,
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
                query = query.Where(u =>
                    u.UserName != null && u.UserName.ToLower().Contains(searchTerm) ||
                    u.FullName != null && u.FullName.ToLower().Contains(searchTerm) ||
                    u.Email != null && u.Email.ToLower().Contains(searchTerm)
                );
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
        public void DeleteUser(string userId)
        {
            var model = _unitOfWork.GenericRepository<ApplicationUser>().GetById(userId);
            _unitOfWork.GenericRepository<ApplicationUser>().Delete(model);
            _unitOfWork.Save();
        }
        public ApplicationUserViewModel GetUserById(string id)
        {
            var model = _unitOfWork.GenericRepository<ApplicationUser>().GetById(id);
            var vm = new ApplicationUserViewModel(model);
            return vm;
        }
        public void UpdateUser(ApplicationUserViewModel userViewModel)
        {
            var ModelById = _unitOfWork.GenericRepository<ApplicationUser>().GetById(userViewModel.Id);
            ModelById.PhoneNumber = userViewModel.PhoneNumber;
            ModelById.FullName = userViewModel.FullName;
            ModelById.Email = userViewModel.Email;
            ModelById.IsActive = userViewModel.IsActive;
            ModelById.DateOfBirth = userViewModel.DateOfBirth;
            ModelById.Specialization = userViewModel.Specialization;

            _unitOfWork.GenericRepository<ApplicationUser>().Update(ModelById);
            _unitOfWork.Save();
        }
        public async Task<ApplicationUserViewModel> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return null;
            }
            var application_user = _unitOfWork.GenericRepository<ApplicationUser>().GetById(user.Id);
            return new ApplicationUserViewModel(application_user);
        }

    }
}
