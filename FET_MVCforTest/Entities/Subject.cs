namespace FET_MVCforTest.Entities
{
	public class Subject
	{
		public int Id { get; set; }
		public string FullScientificName { get; set; } = null!;
		public string Name { get; set; } = null!;
		
		// العلاقة مع القسم
		public int DepartmentId { get; set; }
		public Department Department { get; set; }
	}
}