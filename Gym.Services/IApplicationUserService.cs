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
        PagedResult<ApplicationUserViewModel> GetAll(int PageNumber, int PageSize);
        PagedResult<ApplicationUserViewModel> GetAllTrainer(int PageNumber, int PageSize);
        PagedResult<ApplicationUserViewModel> GetAllCustomer(int PageNumber, int PageSize);
        PagedResult<ApplicationUserViewModel> GetAllReceptionist(int PageNumber, int PageSize);
        PagedResult<ApplicationUserViewModel> SearchTrainer(int PageNumber, int PageSize, string Spicility);
        PagedResult<ApplicationUserViewModel> SearchUsers(string searchTerm, int pageNumber, int pageSize);
    }
}
