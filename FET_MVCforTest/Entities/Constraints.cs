using System.ComponentModel.DataAnnotations;

namespace FET_MVCforTest.Entities
{
	public class Constraints
	{
		public int Id { get; set; }
		public string ConstraintName { get; set; } = null!;
		public string ConstraintsDetails { get; set; } = null!;

	}


}
