namespace FET_MVCforTest.Models.GeminiViewModels
{
	public class TeacherData
	{
		public string Name { get; set; }
		public int MaxHoursPerDay { get; set; }
		public int MaxHoursPerWeek { get; set; }
	}

	public class SubjectData
	{
		public string Name { get; set; } = null!;
		public string FullScintificName { get; set; } = null!;

	}

	public class GroupData
	{
		public string Name { get; set; }
		public int NumberOfStudents { get; set; }
	}
	public class RoomData
	{
		public string Name { get; set; }
		public int Capacity { get; set; }
	}

	public class ActivityData
	{
		public string TeacherName { get; set; }
		public string SubjectName { get; set; }
		public string GroupName { get; set; }
		public int Duration { get; set; }
		public  int NumOfSessionsPerWeek { get; set; }
	}
}
