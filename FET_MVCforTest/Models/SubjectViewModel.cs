using System.ComponentModel.DataAnnotations;

namespace FET_MVCforTest.Models
{
	public class SubjectViewModel
	{
		public int Id { get; set; }


		[Required(ErrorMessage = "Subject name is required.")]
		[StringLength(100, ErrorMessage = "Subject name cannot exceed 100 characters.")]
		[Display(Name = "Subject Full Scientific Name")]

		public string FullScientificName { get; set; } = null!;

		[Required(ErrorMessage = "Subject name is required.")]
		[StringLength(100, ErrorMessage = "Subject name cannot exceed 50 characters.")]
		[Display(Name= "Subject Shortcut Name")]
		public string Name { get; set; }


		[Required(ErrorMessage = "Department is required.")]
		[Display(Name = "Department")]
		public int DepartmentId { get; set; }
	}
}