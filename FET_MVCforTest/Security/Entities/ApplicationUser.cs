using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FET_MVCforTest.Security.Entities
{
	public class ApplicationUser : IdentityUser
	{
		[Display(Name = "First Name")]
		public string FirstName { get; set; } = null!;
		
		[Display(Name = "First Name")] 
		public string LastName { get; set; } =null!;
        public bool IsAgree { get; set; }
    }
}
