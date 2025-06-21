using System.ComponentModel.DataAnnotations;

namespace FET_MVCforTest.Models
{
	public class TeacherViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Teacher Name is required")]
		[StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
		public string Name { get; set; }

		[Range(1, 12, ErrorMessage = "Max hours per day must be between 1 and 12")]
		[Display(Name = "Max Hours Per Day")]
		public int? MaxHoursPerDay { get; set; } =8;

		[Range(1, 60, ErrorMessage = "Max hours per week must be between 1 and 60")]
		[Display(Name = "Max Hours Per Week")]
		public int? MaxHoursPerWeek { get; set; } = 48;
	}
}
