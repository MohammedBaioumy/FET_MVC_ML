using FET_MVCforTest.Security.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FET_MVCforTest.Security
{
	public class DataSeeding(
		                     ILoggerFactory loggerFactory,
		                     UserManager<ApplicationUser> userManager,
							 RoleManager<IdentityRole> roleManager,
							 AuthIdentityDbContext storeIdentityDbContext) : IDataSeeding
	{
		

		public async Task SeedIdentiyDataAsync()
		{
			try
			{
				if ((await storeIdentityDbContext.Database.GetPendingMigrationsAsync()).Any())
					await storeIdentityDbContext.Database.MigrateAsync();
				// Create roles
				if (roleManager.Roles.Count() == 0)
				{
					await roleManager.CreateAsync(new IdentityRole("Admin"));
					await roleManager.CreateAsync(new IdentityRole("User"));
				}
				
				if (userManager.Users.Count() == 0)
				{
					var AppUser01 = new ApplicationUser()
					{
						FirstName="Admin",
						LastName = "FET Admin",
						Email = "FET_Admin1@gmail.com",
						PhoneNumber = "0115000898",
						UserName = "FET_Admin1"
					};
					var AppUser02 = new ApplicationUser()
					{
						FirstName = "User",
						LastName = "FET User",
						Email = "FET_DefaultUser@gmail.com",
						PhoneNumber = "01004538457",
						UserName = "FET_DefaultUser1"
					};
					await userManager.CreateAsync(AppUser01, "P@ssw0rd");
				    await userManager.AddToRoleAsync(AppUser01, "Admin");
					await userManager.CreateAsync(AppUser02, "P@ssw0rd");
					await userManager.AddToRoleAsync(AppUser02, "User");
					
				}

				await storeIdentityDbContext.SaveChangesAsync();


			}
			catch (Exception ex)
			{
				   loggerFactory.CreateLogger<DataSeeding>()
					.LogError(ex,ex.Message);
			}
		}
	}
}
