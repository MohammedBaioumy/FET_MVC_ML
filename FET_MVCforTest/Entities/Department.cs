

namespace FET_MVCforTest.Entities
{
	public class Department
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public int FacultyId { get; set; }
		public Faculty Faculty { get; set; }

		// العلاقة مع المواد
		public ICollection<Subject> Subjects { get; set; }
	}
}