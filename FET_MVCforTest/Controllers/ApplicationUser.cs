using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baiomy.FCI2.DAL.Entities.Identity
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
