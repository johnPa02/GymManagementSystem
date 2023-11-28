using System.Data;
using Gym.Services;
using Gym.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Gym.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CustomerPackagesController : Controller
    {
        private readonly ICustomerPackageService _customerPackageService;
        private readonly IPackageService _packageService;
        private readonly IApplicationUserService _applicationUserService;

        public CustomerPackagesController(ICustomerPackageService customerPackageService,
            IApplicationUserService applicationUserService,
            IPackageService packageService)
        {
            _customerPackageService = customerPackageService;
            _applicationUserService = applicationUserService;
            _packageService = packageService;
        }
        [Route("/Admin/CustomerPackages/Management")]
        public IActionResult Index()
        {
            var customerPackages = _customerPackageService.GetAll(1, 10);
            return View(customerPackages);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CustomerPackageViewModel
            {
                Packages = _packageService.GetAllPackages().Select(p => new SelectListItem
                {
                    Value = p.PackageId.ToString(),
                    Text = p.PackageName
                })
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerPackageViewModel viewModel)
        {
            ModelState.Remove("CustomerId");
            if (viewModel.StartDate >= viewModel.EndDate)
            {
                ModelState.AddModelError("", "Ngày bắt đầu phải nhỏ hơn ngày kết thúc.");
            }
            if (ModelState.IsValid)
            {
                var user = await _applicationUserService.GetUserByEmailAsync(viewModel.Email);
                if (user != null)
                {
                    viewModel.CustomerId = user.Id;

                    _customerPackageService.InsertCustomerPackage(viewModel);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Email", "Khách hàng này chưa đăng kí");
                }
            }
            viewModel.Packages = _packageService.GetAllPackages().Select(p => new SelectListItem
            {
                Text = p.PackageName,
                Value = p.PackageId.ToString()
            });
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var customerPackage = _customerPackageService.GetCustomerPackageById(id);
            if (customerPackage == null)
            {
                return NotFound();
            }

            customerPackage.Packages = _packageService.GetAllPackages().Select(p => new SelectListItem
            {
                Value = p.PackageId.ToString(),
                Text = p.PackageName
            });
            return View(customerPackage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CustomerPackageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _applicationUserService.GetUserById(viewModel.CustomerId);
                if (user != null)
                {
                    _customerPackageService.UpdateCustomerPackage(viewModel);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Invalid customer ID.");
                }
            }
            viewModel.Packages = _packageService.GetAllPackages().Select(p => new SelectListItem
            {
                Value = p.PackageId.ToString(),
                Text = p.PackageName
            });

            return View(viewModel);
        }
        public IActionResult Delete(int id)
        {
            var customerPackage = _customerPackageService.GetCustomerPackageById(id);

            if (customerPackage == null)
            {
                return NotFound("Customer package not found");
            }

            _customerPackageService.DeleteCustomerPackage(id);
            return RedirectToAction(nameof(Index));
        }
    }


}
