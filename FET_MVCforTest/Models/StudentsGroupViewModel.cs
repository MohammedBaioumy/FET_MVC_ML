using System.ComponentModel.DataAnnotations;

namespace FET_MVCforTest.Models
{
	public class StudentsGroupViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Students Group name is required.")]
		[StringLength(100, ErrorMessage = "Students Group name cannot exceed 100 characters.")]
		public string Name { get; set; }

		[Range(1, int.MaxValue, ErrorMessage = "Number of students must be greater than zero.")]
		public int? NumberOfStudents { get; set; }
	}

}
