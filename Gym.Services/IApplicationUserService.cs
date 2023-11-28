using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gym.Utillities;
using Gym.ViewModels;

namespace Gym.Services
{
    public interface IApplicationUserService
    {
        void DeleteUser(string userId);
        ApplicationUserViewModel GetUserById(string userId);
        Task<ApplicationUserViewModel> GetUserByEmailAsync(string email);
        void UpdateUser(ApplicationUserViewModel user);
        PagedResult<ApplicationUserViewModel> GetAll(int PageNumber, int PageSize);
        PagedResult<ApplicationUserViewModel> GetUsersByRole(int PageNumber, int PageSize, string role);
        PagedResult<ApplicationUserViewModel> GetAllCustomer(int PageNumber, int PageSize);
        PagedResult<ApplicationUserViewModel> GetAllReceptionist(int PageNumber, int PageSize);
        PagedResult<ApplicationUserViewModel> SearchTrainer(int PageNumber, int PageSize, string Spicility);
        PagedResult<ApplicationUserViewModel> SearchUsers(string searchTerm, string role, int pageNumber, int pageSize);
    }
}
