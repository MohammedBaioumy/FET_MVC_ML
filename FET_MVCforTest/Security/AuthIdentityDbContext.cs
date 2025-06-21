using FET_MVCforTest.Security.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FET_MVCforTest.Security
{
	public class AuthIdentityDbContext(DbContextOptions<AuthIdentityDbContext> options)  : IdentityDbContext<ApplicationUser>(options)
	{

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);	
			builder.Entity<IdentityUser>().ToTable("Users");
			builder.Entity<IdentityRole>().ToTable("Roles");
			builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
			
		}


	}
}
