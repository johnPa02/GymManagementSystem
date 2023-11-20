﻿using Gym.Services;
using Gym.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class PackagesController : Controller
	{
		private IPackageService _package;
		public PackagesController(IPackageService package)
		{
			_package = package;
		}

		[Route("/Admin/PackageManagement")]
		public IActionResult Index(int pageNumer=1, int pageSize=10)
		{
			return View(_package.GetAll(pageNumer, pageSize));
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			var model = _package.GetPackageById(id);
			return View(model);
		}
		[HttpPost]
		public IActionResult Edit(PackageViewModel packageViewModel)
		{
			_package.UpdatePackage(packageViewModel);
			return RedirectToAction("Index");
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(PackageViewModel packageViewModel) 
		{
			_package.InsertPackage(packageViewModel);
			return RedirectToAction("Index");
		}
		public IActionResult Delete(int id)
		{
			_package.DeletePackage(id);
			return RedirectToAction("Index");
		}
	}
}
