using System.ComponentModel.DataAnnotations;

namespace FET_MVCforTest.Models
{
	public class BasicViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Table name is required.")]
		[Display(Name="Table Name")]
		public string Name { get; set; } = null!;
		
		[Display(Name = "Start Day Of Week")]
		public DayOfWeek? startOfWeek { get; set; }
		[Display(Name = "End Day Of Week")]
		public DayOfWeek? endOfWeek { get; set; }

		[Display(Name = "Start Time")]
		[Required(ErrorMessage = "Start Time is required.")]

		public TimeOnly TimeStart { get; set; } = TimeOnly.FromTimeSpan(TimeSpan.FromHours(9));
		[Display(Name = "End Time")]
		[Required(ErrorMessage = "Start Time is required.")]

		public TimeOnly TimeEnd { get; set; } = TimeOnly.FromTimeSpan(TimeSpan.FromHours(5));
	}
}
