namespace FET_MVCforTest.Entities
{
	public class Teacher
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public int? MaxHoursPerDay { get; set; }
		public int? MaxHoursPerWeek { get; set; }
	}
}
