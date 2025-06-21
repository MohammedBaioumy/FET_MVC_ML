using System.ComponentModel.DataAnnotations;

namespace FET_MVCforTest.Models
{
	public class DepartmentViewModel
	{		

		[Required(ErrorMessage = "Department name is required.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Faculty ID is required.")]
		
		public int FacultyId { get; set; }
		
	}
}
