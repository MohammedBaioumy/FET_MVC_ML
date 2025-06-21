using FET_MVCforTest.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FET_MVCforTest.Models
{
	public class ConstraintViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Please provide constraint Name.")]
		[Display(Name = "Constraints Name")]
		public string ConstraintName { get; set; } = null!;


		[Required(ErrorMessage = "Please provide constraint details.")]
		[Display(Name = "Constraints Details")]
		public string ConstraintsDetails { get; set; } = null!;

	}
}
