namespace FET_MVCforTest.Entities
{
	public class Basic
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;	
		public DayOfWeek? startOfWeek { get; set; }
		public DayOfWeek? endOfWeek { get; set; }

		public TimeOnly TimeStart { get; set; } = TimeOnly.FromTimeSpan(TimeSpan.FromHours(9));
		public TimeOnly TimeEnd { get; set; } = TimeOnly.FromTimeSpan(TimeSpan.FromHours(5));


	}
}
