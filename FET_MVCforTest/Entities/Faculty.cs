namespace FET_MVCforTest.Entities
{
	public class Faculty
	{
		public int Id { get; set; }
		public string Name { get; set; }

		// Navigation property
		public ICollection<Department> Departments { get; set; }
	}
}
