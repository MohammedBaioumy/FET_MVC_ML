using System.ComponentModel.DataAnnotations;

namespace FET_MVCforTest.Models
{
	public class SignUpViewModel
	{
        [Display(Name = "User Name")]

		public string UserName { get; set; } = null!;


		[DataType(DataType.EmailAddress)]
		public string Email { get; set; } = null!;


		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;


		[Compare("Password")]
		public string ConfirmPassword { get; set; } = null!;


        [Display(Name = "First Name")]
		public string FirstName { get; set; } = null!;

		[Display(Name = "First Name")]
		public string LastName { get; set; } = null!;
		public bool IsAgree { get; set; }


	}
}
