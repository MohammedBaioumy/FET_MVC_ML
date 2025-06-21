using System.ComponentModel.DataAnnotations;

namespace FET_MVCforTest.Models
{
	public class FacultyViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Faculty name is required.")]
		public string Name { get; set; }
	}
}
