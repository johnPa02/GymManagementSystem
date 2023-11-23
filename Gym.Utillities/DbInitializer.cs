
using Gym.Models;
using Gym.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gym.Utillities
{
	public class DbInitializer : IDbInitializer
	{
		private UserManager<IdentityUser> _userManager;
		private RoleManager<IdentityRole> _roleManager;
		private ApplicationDbContext _context;

		public DbInitializer(UserManager<IdentityUser> userManager,
			RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_context = context;
		}

		public void Initialize()
		{
			try
			{
				if (_context.Database.GetPendingMigrations().Count() > 0)
				{
					_context.Database.Migrate();
				}
			}
			catch (Exception) { throw; }
			if (!_roleManager.RoleExistsAsync(WebSiteRoles.Website_Admin).GetAwaiter().GetResult())
			{
				_roleManager.CreateAsync(new IdentityRole(WebSiteRoles.Website_Admin)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(WebSiteRoles.Website_Trainer)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(WebSiteRoles.Website_Receptionist)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(WebSiteRoles.Website_Customer)).GetAwaiter().GetResult();

				_userManager.CreateAsync(new ApplicationUser
				{
					UserName= "admin@gmail.com",
					Email= "admin@gmail.com",
                    EmailConfirmed = true
                }, "Admin@123").GetAwaiter().GetResult();
				var Appuser = _context.ApplicationUsers.FirstOrDefault(x=>x.Email == "admin@gmail.com");
				if(Appuser != null)
				{
					_userManager.AddToRoleAsync(Appuser, WebSiteRoles.Website_Admin).GetAwaiter().GetResult();
				}
			}
		}
	}
}
