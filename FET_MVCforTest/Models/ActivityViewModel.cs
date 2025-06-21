using System.ComponentModel.DataAnnotations;

namespace FET_MVCforTest.Models
{
	public class ActivityViewModel
	{		

		[Required(ErrorMessage = "Teacher is required.")]
		public int TeacherId { get; set; }

		[Required(ErrorMessage = "Subject is required.")]
		public int SubjectId { get; set; }

		[Required(ErrorMessage = "Students Group is required.")]
		public int StudentsGroupId { get; set; }

		
		[Range(1, int.MaxValue, ErrorMessage = "Duration must be greater than 0.")]
		public int? duration { get; set; } = 120;
		public int? Duration
		{
			get { return duration; }
			set { duration = value is null ? 120 : value; }
		}

		//	public int? NumOfSessionsPerWeek { get; set}= 1;
		private int? numOfSessionsPerWeek;
		[Range(1, 7, ErrorMessage = "Number Of Seesions Per Week Must Be Between 1 And 7 ")]
		[Display(Name = "Number of seesions per week")]
		public int? NumOfSessionsPerWeek
		{
			get { return numOfSessionsPerWeek; }
			set { numOfSessionsPerWeek = value is null?1:value; }
		}


	}
}
