using System.ComponentModel.DataAnnotations;

namespace FET_MVCforTest.Models
{
	public class RoomViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Room name is required.")]
		[StringLength(100, ErrorMessage = "Room name must be less than 100 characters.")]
		[Display(Name = "Room Name")]
		public string Name { get; set; }

		[Range(1, int.MaxValue, ErrorMessage = "Capacity must be a positive number.")]
		public int? Capacity { get; set; }
	}
}
