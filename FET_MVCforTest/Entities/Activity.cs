namespace FET_MVCforTest.Entities
{
	public class Activity
	{
		public int Id { get; set; }

		public int TeacherId { get; set; }
		public Teacher Teacher { get; set; }

		public int SubjectId { get; set; }
		public Subject Subject { get; set; }

		public int StudentsGroupId { get; set; }
		public StudentsGroup StudentsGroup { get; set; }

		public int Duration { get; set; } = 120;
		public int? NumOfSessionsPerWeek { get; set; } = 1;

	}
}
