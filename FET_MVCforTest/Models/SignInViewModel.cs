using System.ComponentModel.DataAnnotations;

namespace FET_MVCforTest.Models
{
	public class SignInViewModel
	{

		[EmailAddress]
		public string Email { get; set; } = null!;


		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;

		[Display(Name ="Remember me")]
		public bool RememberMe { get; set; }
	}
}
